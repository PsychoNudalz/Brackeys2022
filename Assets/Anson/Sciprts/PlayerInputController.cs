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

    [SerializeField]
    private PlayerCameraController playerCamController;

    [Space(10)]
    [SerializeField]
    private CharacterControllerScript characterController;

    [SerializeField]
    private CharacterMovementController characterMovementController;

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

    private GameManager gm;

    public PlayerController PlayerController
    {
        get => playerController;
        set => playerController = value;
    }

    public PlayerCameraController PlayerCamController
    {
        get => playerCamController;
        set => playerCamController = value;
    }

    public CharacterControllerScript CharacterController
    {
        get => characterController;
        set => characterController = value;
    }

    public CharacterMovementController CharacterMovementController
    {
        get => characterMovementController;
        set => characterMovementController = value;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void LateUpdate()
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

        characterMovementController.Move(moveDir);
    }

    public void OnSwitchCharacter(InputValue context)
    {
        if (gm.characterDeaths < 2)
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
    }

    public void OnRotateCamera(InputValue context)
    {
        float value = context.Get<float>();
        if (value > 0)
        {
            playerCamController.turnCamAntiClockwise();
        }
        else if (value < 0)
        {
            playerCamController.turnCamClockwise();
        }
    }

    public RaycastHit RaycastFromCameraToMouse()
    {
        Ray ray;
        if (mainCamera.orthographic)
        {
            ray = new Ray(mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()),
                mainCamera.transform.forward);
        }
        else
        {
            ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            
            
        }

        float mouseCastDistance = 1000f;
        Debug.DrawRay(ray.origin, ray.direction * mouseCastDistance, Color.cyan, 1f);
        Physics.Raycast(ray, out var raycastHit, mouseCastDistance, mouseLayer);


        if (raycastHit.collider && mouseTags.Contains(raycastHit.collider.tag))
        {
            ClickableObject temp = raycastHit.collider.GetComponentInParent<ClickableObject>();
            if (temp)
            {
                hoverObject = temp;
                hoverObject.OnHover(true);
            }
            else
            {
                hoverObject?.OnHover(false);
                hoverObject = null;
            }
        }
        else
        {
            hoverObject?.OnHover(false);
            hoverObject = null;
        }


        // Debug.DrawRay(mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()),mainCamera.transform.forward*1000f);

        return raycastHit;
    }

    public void OnMouseClick(InputValue context)
    {
        StartCoroutine(SelectObjectCoroutine());
    }

    private IEnumerator SelectObjectCoroutine()
    {
        if (selectObject)
        {
            selectObject.OnSelect(false);
        }

        if (hoverObject)
        {
            hoverObject.OnSelect(true);
        }

        selectObject = hoverObject;
        yield return new WaitForEndOfFrame();
    }

    public void OnClimb()
    {
        characterMovementController.Climb();
    }

    public void OnUseInteractable()
    {
        characterController.OnUseInteractable();
    }
}