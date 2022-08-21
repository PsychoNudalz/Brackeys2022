using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float climbHeight = 1;
    
    
    [Header("Current Stats")]
    private Vector3 moveDir = new Vector3();
    
    [Header("Components")]
    [SerializeField]
    private CharacterController characterController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDir.magnitude > 0.1f)
        {
            characterController.Move(moveDir * (speed * Time.deltaTime));
            transform.forward = moveDir;
        }
    }

    public void Move(Vector3 moveDir)
    {
        this.moveDir = moveDir.normalized;
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
}
