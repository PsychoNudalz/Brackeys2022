using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [Header("Mouse Settings")]
    [SerializeField]
    private List<String> mouseTags;

    [SerializeField]
    private LayerMask mouseLayer;

    [SerializeField]
    private ClickableObject hoverObject;

    [SerializeField]
    private ClickableObject selectObject;

    [Header("Components")]
    [SerializeField]
    private Camera mainCamera;

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
        mainCamera = Camera.main;
    }

    private void Update()
    {
        RaycastHit raycastHit = RaycastFromCameraToMouse();

    }

    public void OnMovement(InputValue context)
    {
        Vector3 moveDir = context.Get<Vector2>();
        if (moveDir.magnitude > 0.1f)
        {
            // float targetAngle = Mathf.Atan2(moveDir.x, moveDir.y) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            // float angle = Mathf.SmoothDamp(())
            moveDir = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f) *
                      new Vector3(moveDir.x, 0, moveDir.y);
        }

        currentCharacterControllerScript.Move(moveDir);
    }

    public void OnSwitchCharacter(InputValue context)
    {
        float value = context.Get<float>();
        if (value > 0)
        {
            playerController.NextCharacter();
        }
        else if (value < 0)
        {
            playerController.PrevCharacter();
        }
    }

    public RaycastHit RaycastFromCameraToMouse()
    {
        Ray ray = new Ray(mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()),mainCamera.transform.forward);

        Physics.Raycast(ray, out var raycastHit, 1000f, mouseLayer);
        
        
        if (raycastHit.collider && mouseTags.Contains(raycastHit.collider.tag))
        {
            ClickableObject temp =  raycastHit.collider.GetComponentInParent<ClickableObject>();
            if (temp)
            {
                hoverObject = temp;
            }
            else
            {
                hoverObject = null;
            }
        }
        else
        {
            hoverObject = null;
        }
        // Debug.DrawRay(mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()),mainCamera.transform.forward*1000f);

        return raycastHit;
    }

    public void OnMouseClick(InputValue context)
    {
        selectObject = hoverObject;
    }
}