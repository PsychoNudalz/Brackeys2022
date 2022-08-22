using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private CharacterMovementController characterMovementController;

    public CharacterMovementController CharacterMovementController => characterMovementController;

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