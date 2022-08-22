using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterManager characterManager;

    [SerializeField]
    private CharacterControllerScript[] characterControllers;


    [SerializeField]
    private CharacterControllerScript currentCharacter;

    [SerializeField]
    private int characterIndex = 0;

    public CharacterControllerScript CurrentCharacter => currentCharacter;

    [Header("Player Components")]
    [SerializeField]
    private PlayerInputController playerInputController;


    [ContextMenu("Initialise Components")]
    public void InitialiseComponents()
    {
        playerInputController = GetComponentInChildren<PlayerInputController>();
        playerInputController.PlayerController = this;

        if (characterControllers.Length > 0)
        {
            SetCharacter(characterControllers[characterIndex]);
        }
    }

    private void Start()
    {
        if (characterControllers.Length > 0)
        {
            SetCharacter(characterControllers[characterIndex]);
        }
    }


    public void SetCharacter(CharacterControllerScript characterControllerScript)
    {
        currentCharacter = characterControllerScript;
        playerInputController.CharacterMovementController = currentCharacter.CharacterMovementController;
    }

    public void NextCharacter()
    {
        characterIndex++;
        characterIndex = characterIndex % characterControllers.Length;
        SetCharacter(characterControllers[characterIndex]);
    }
    public void PrevCharacter()
    {
        characterIndex--;
        characterIndex = (characterIndex+characterControllers.Length) % characterControllers.Length;
        SetCharacter(characterControllers[characterIndex]);
    }
}