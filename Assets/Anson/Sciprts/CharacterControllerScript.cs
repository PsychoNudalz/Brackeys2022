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

/// <summary>
/// Controls individual character
/// </summary>
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
    private CharacterEffectsController characterEffectsController;

    [SerializeField]
    private CharacterShieldController characterShieldController;

    public CharacterMovementController CharacterMovementController => characterMovementController;

    public CharacterAbilityHandler CharacterAbilityHandler => characterAbilityHandler;

    public CharacterEffectsController CharacterEffectsController => characterEffectsController;

    public CharacterShieldController CharacterShieldController => characterShieldController;

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

        if (!characterShieldController)
        {
            characterShieldController = GetComponent<CharacterShieldController>();
        }

        if (!characterEffectsController)
        {
            characterEffectsController = GetComponentInChildren<CharacterEffectsController>();
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
                characterEffectsController.Interact();
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
            characterEffectsController.SetModelSelectHighlight(true);
        }
        else
        {
            characterMovementController.Move(new Vector3());
            characterEffectsController.SetModelSelectHighlight(false);
        }
    }

    public void killCharacter() //disables movement, switches to the next character (if possible) and disabled switching back
    {
        print(name +" is dead");
        aliveEnum = AliveEnum.Dead;
        SetActive(false);
        // play death animation
        //
        // characterMovementController.enabled = false;
        // GetComponent<CharacterController>().enabled = false;
        characterMovementController.SetControlLock(true);
    }

    public void SetShield(bool b)
    {
        Debug.Log(name+" Shield: "+b);
        characterShieldController.SetActive(b);
    }
}