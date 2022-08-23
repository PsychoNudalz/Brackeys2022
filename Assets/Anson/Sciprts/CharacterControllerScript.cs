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


    public CharacterMovementController CharacterMovementController => characterMovementController;

    public CharacterAbilityHandler CharacterAbilityHandler => characterAbilityHandler;

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
    }

    public void SlantCharacter(float slantDegree, Transform mainCamera)
    {
        characterMovementController.SlantCharacter(slantDegree, mainCamera);
    }

    public void OnUseInteractable()
    {
        interactTriggerDetector.InteractableObject?.OnUse();
    }

    public Vector3 GetCentrePosition()
    {
        return centreOffset + transform.position;
    }
    
    
}