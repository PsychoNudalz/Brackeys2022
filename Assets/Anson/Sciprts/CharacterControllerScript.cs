using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterEnum
{
    Soldier,
    Archer,
    Mage
}

public enum AliveEnum
{
    Alive,
    Dead
}

public enum SuppressionEnum
{
    None,
    Suppressed
}


public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField]
    private CharacterEnum characterEnum;

    [Space(10)]
    [Header("States")]
    [SerializeField]
    private AliveEnum aliveEnum;

    [SerializeField]
    private SuppressionEnum suppressionEnum;

    [SerializeField]
    private Vector3 centreOffset = new Vector3(0, 1, 0);


    [Header("Components")]
    [SerializeField]
    private CharacterMovementController characterMovementController;

    [SerializeField]
    private InteractTriggerDetector interactTriggerDetector;

    [SerializeField]
    private CharacterAbilityHandler characterAbilityHandler;

    [SerializeField]
    private CharacterAvatarController characterAvatarController;


    public CharacterMovementController CharacterMovementController => characterMovementController;

    public CharacterAbilityHandler CharacterAbilityHandler => characterAbilityHandler;

    public CharacterAvatarController CharacterAvatarController => characterAvatarController;

    public CharacterEnum CharacterEnum => characterEnum;

    public AliveEnum Alive => aliveEnum;

    public SuppressionEnum Suppression => suppressionEnum;

    public Vector3 CentreOffset => centreOffset;


    private void Awake()
    {
        if (!characterMovementController)
        {
            characterMovementController = GetComponent<CharacterMovementController>();
        }

        if (!interactTriggerDetector)
        {
            interactTriggerDetector = GetComponent<InteractTriggerDetector>();
        }

        if (!characterAbilityHandler)
        {
            characterAbilityHandler = GetComponent<CharacterAbilityHandler>();
        }

        if (!characterAvatarController)
        {
            characterAvatarController = GetComponentInChildren<CharacterAvatarController>();
        }
    }

    public void SlantCharacter(float slantDegree, Transform mainCamera)
    {
        characterMovementController.SlantCharacter(slantDegree, mainCamera);
    }

    public void OnUseInteractable()
    {
        if (interactTriggerDetector.InteractableObject)
        {
            if (interactTriggerDetector.InteractableObject.OnUse())
            {
                characterAvatarController.Interact();
            }
        }
    }

    public Vector3 GetCentrePosition()
    {
        return centreOffset + transform.position;
    }

    public void SetActive(bool b)
    {
        if (b)
        {
            
        }
        else
        {
            characterMovementController.Move(new Vector3());
        }
    }
    
    
}