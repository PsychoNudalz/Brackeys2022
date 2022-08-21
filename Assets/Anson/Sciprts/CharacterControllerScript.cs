using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    
    [Header("Movement")]
    [SerializeField]
    private float speed = 5;

    [Header("Climbing")]
    [SerializeField]
    private float climbHeight = 1;

    [SerializeField]
    private float heightOffset = 2;

    [SerializeField]
    private float climbCastCheckAngle = 45f;

    [SerializeField]
    private LayerMask climbLayer;

    [SerializeField]
    private float climbTime = .5f;

    [SerializeField]
    private float climbSpeed = 5f;

    private float originalCCSlop;
    private float originalCCStep;
    private bool climbFlag = false;


    [Header("Current Stats")]
    private Vector3 moveDir = new Vector3();

    private bool controlLock = false;

    [Header("Components")]
    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private Transform slantTransform;

    [SerializeField]
    private Transform modelTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (characterController)
        {
            originalCCSlop = characterController.slopeLimit;
            originalCCStep = characterController.stepOffset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDir.magnitude > 0.1f)
        {

            
            characterController.Move(moveDir * (speed * Time.deltaTime));
            modelTransform.localRotation = Quaternion.Euler(0,
                Vector3.SignedAngle(slantTransform.forward, moveDir, slantTransform.up), 0);
        }

        if (!climbFlag)
        {
            UpdateGravity();
        }
    }

    public void Move(Vector3 moveDir)
    {
        if (!controlLock)
        {
            this.moveDir = moveDir.normalized;
        }
    }


    [ContextMenu("Initialise Components")]
    public void InitialiseComponents()
    {
        characterController = GetComponentInChildren<CharacterController>();
        if (!characterController)
        {
            Debug.LogError("Cannot find character controller");
        }
    }

    public void SlantCharacter(float slantDegree, Transform camera)
    {
        slantTransform.rotation =
            Quaternion.Euler(0f, camera.eulerAngles.y, 0f) * Quaternion.Euler(slantDegree, 0, 0);
    }

    public void Climb()
    {
        if (CanClimb())
        {
            StartCoroutine(ClimbCoroutine());
        }
    }

    IEnumerator ClimbCoroutine()
    {
        climbFlag = true;
        controlLock = true;
        characterController.slopeLimit = 85f;
        characterController.stepOffset = climbHeight * 2f;
        moveDir = (Quaternion.AngleAxis(-50f,modelTransform.right)*modelTransform.forward).normalized;
        yield return new WaitForSeconds(climbTime);
        moveDir = new Vector3();
        characterController.slopeLimit = originalCCSlop;
        characterController.stepOffset = originalCCStep;
        climbFlag = false;
        controlLock = false;
    }

    bool CanClimb()
    {
        if (climbFlag)
        {
            return false;
        }
        
        RaycastHit hit;
        float CastDistance = 2f;
        Vector3 startPosition = transform.position + new Vector3(0f, heightOffset + climbHeight, 0f);
        Vector3 rayDirection = Quaternion.AngleAxis(climbCastCheckAngle, modelTransform.right) * modelTransform.forward;
        if (Physics.Raycast(startPosition,
                rayDirection, out hit, CastDistance,
                climbLayer))
        {
            if (Vector3.Dot(hit.normal, modelTransform.up) > .9f)
            {
                return true;
            }
        }

        Debug.DrawRay(startPosition,
            rayDirection * CastDistance, Color.blue,2f);

        return false;
    }

    void UpdateGravity()
    {
        characterController.Move(new Vector3(0, -5f*Time.deltaTime, 0));
    }
}