using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character Controllers")]
    [SerializeField]
    private CharacterControllerScript[] characterControllers;

    [SerializeField]
    private CharacterControllerScript currentCharacter;

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
            SetCharacter(characterControllers[0]);
        }
    }


    public void SetCharacter(CharacterControllerScript characterControllerScript)
    {
        currentCharacter = characterControllerScript;
        playerInputController.CurrentCharacterControllerScript = currentCharacter;
    }
    
    
    
    
}