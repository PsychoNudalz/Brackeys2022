using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    
    enum CharacterState
    {
        Idle,
        Controlled,
        Climbing,
        Falling
    };

    [SerializeField]
    private CharacterState characterState = CharacterState.Controlled;

    [Header("Movement")]
    [SerializeField]
    private float speed = 5;

    [Header("Gravity & Falling")]
    [SerializeField]
    private float gravity = 6f;

    [SerializeField]
    private float groundCheckDistance = .3f;

    [SerializeField]
    private LayerMask groundLayer;

    private bool isGrounded = true;

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
        if (characterState != CharacterState.Climbing)
        {
            UpdateGrounded();
            UpdateGravity();
        }

        if (moveDir.magnitude > 0.1f)
        {
            characterController.Move(moveDir * (speed * Time.deltaTime));
            if (characterState == CharacterState.Climbing)
            {
                
            }
            else
            {
                modelTransform.localRotation = Quaternion.Euler(0,
                    Vector3.SignedAngle(slantTransform.forward, moveDir, slantTransform.up), 0);
            }
        }
    }

    public void Move(Vector3 moveDir)
    {
        if (!(controlLock || characterState is CharacterState.Falling or CharacterState.Climbing))
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
        characterState = CharacterState.Climbing;
        controlLock = true;
        characterController.slopeLimit = 85f;
        characterController.stepOffset = climbHeight * 2f;
        moveDir = (Quaternion.AngleAxis(-50f, modelTransform.right) * modelTransform.forward).normalized;
        yield return new WaitForSeconds(climbTime);
        moveDir = new Vector3();
        characterController.slopeLimit = originalCCSlop;
        characterController.stepOffset = originalCCStep;
        characterState = CharacterState.Controlled;
        controlLock = false;
    }

    bool CanClimb()
    {
        if (characterState == CharacterState.Climbing)
        {
            return false;
        }

        RaycastHit hit;
        float CastDistance = 3f;
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
            rayDirection * CastDistance, Color.blue, 2f);

        return false;
    }

    void UpdateGravity()
    {
        characterController.Move(new Vector3(0, -gravity * Time.deltaTime, 0));
    }

    bool UpdateGrounded()
    {
        isGrounded = Physics.CheckSphere(transform.position,groundCheckDistance,groundLayer,QueryTriggerInteraction.Ignore);
        // isGrounded = Physics.Raycast(transform.position+new Vector3(0,.1f,0),  Vector3.down, out var raycastHit,
        //     groundCheckDistance , groundLayer);

        if (!isGrounded && characterState == CharacterState.Controlled)
        {
            characterState = CharacterState.Falling;
            moveDir = new Vector3();
        }
        else if (isGrounded && characterState == CharacterState.Falling)
        {
            characterState = CharacterState.Controlled;
        }

        return isGrounded;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position,groundCheckDistance);
    }
}
