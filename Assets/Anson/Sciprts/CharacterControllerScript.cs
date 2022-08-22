using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("States")]
    [SerializeField]
    private AliveEnum aliveEnum;

    [SerializeField]
    private SuppressionEnum suppressionEnum;
    
    [Header("Components")]
    [SerializeField]
    private CharacterMovementController characterMovementController;

    public CharacterMovementController CharacterMovementController => characterMovementController;


    public AliveEnum Alive => aliveEnum;

    public SuppressionEnum Suppression => suppressionEnum;

    private void Awake()
    {
        if (!characterMovementController)
        {
            characterMovementController = GetComponent<CharacterMovementController>();
            
        }
    }

    public void SlantCharacter(float slantDegree, Transform mainCamera)
    {
        characterMovementController.SlantCharacter(slantDegree,mainCamera);
    }
}