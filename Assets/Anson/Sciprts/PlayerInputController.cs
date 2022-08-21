using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [Space(10)]
    [SerializeField]
    private CharacterControllerScript currentCharacterControllerScript;

    [Header("Components")]
    [SerializeField]
    private Transform mainCamera;
    public PlayerController PlayerController
    {
        get => playerController;
        set => playerController = value;
    }

    public CharacterControllerScript CurrentCharacterControllerScript
    {
        get => currentCharacterControllerScript;
        set => currentCharacterControllerScript = value;
    }

    
    private void Awake()
    {
        mainCamera = Camera.main.transform;
        
    }
    
    public void OnMovement(InputValue context)
    {
        Vector3 moveDir = context.Get<Vector2>();
        if (moveDir.magnitude > 0.1f)
        {
            // float targetAngle = Mathf.Atan2(moveDir.x, moveDir.y) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            // float angle = Mathf.SmoothDamp(())
            moveDir = Quaternion.Euler(0f, mainCamera.eulerAngles.y, 0f) * new Vector3(moveDir.x,0,moveDir.y);
        }
        currentCharacterControllerScript.Move(moveDir);
        print(moveDir);
    }

    public void OnSwitchCharacter(InputValue context)
    {
        float value = context.Get<float>();
        if (value > 0)
        {
            playerController.NextCharacter();
        }else if (value < 0)
        {
            playerController.PrevCharacter();
        }
    }
}
