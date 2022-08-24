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
    private CharacterControllerScript[] characterMovementControllers;

    [SerializeField]
    private HighlightPlus.HighlightEffect[] characterHighlights;

    [SerializeField]
    private CharacterControllerScript currentCharacter;

    [SerializeField]
    private int characterIndex = 0;

    public CharacterControllerScript CurrentCharacter => currentCharacter;

    [Header("Player Components")]
    [SerializeField]
    private PlayerInputController playerInputController;

    private HighlightPlus.HighlightEffect currentCharHighlight;

    [ContextMenu("Initialise Components")]
    public void InitialiseComponents()
    {
        if (!playerInputController)
        {
            playerInputController = GetComponentInChildren<PlayerInputController>();
        }

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

        if (currentCharacter)
        {
            currentCharacter.SetActive(false);
        }
        currentCharacter = characterControllerScript;
        playerInputController.CharacterController = currentCharacter;
        playerInputController.CharacterMovementController = currentCharacter.CharacterMovementController;
        currentCharacter.SetActive(true);

        
        //
        // if (currentCharHighlight)
        // {
        //     currentCharHighlight.highlighted = false;
        //     currentCharHighlight = characterHighlights[characterIndex];
        //     StartCoroutine(highlightCharSelect());
        // }
    }

    // private IEnumerator highlightCharSelect()
    // {
    //     currentCharHighlight.highlighted = true;
    //     yield return new WaitForSeconds(0.25f);
    //     currentCharHighlight.highlighted = false;
    //     yield return new WaitForSeconds(0.25f);
    //     currentCharHighlight.highlighted = true;
    //     yield return new WaitForSeconds(0.25f);
    //     currentCharHighlight.highlighted = false;
    //     yield return new WaitForSeconds(0.25f);
    //     currentCharHighlight.highlighted = true;
    //     yield return new WaitForSeconds(0.25f);
    //     currentCharHighlight.highlighted = false;
    // }

    public void NextCharacter()
    {
        characterIndex++;
        characterIndex = characterIndex % characterMovementControllers.Length;

        if (characterMovementControllers[characterIndex].enabled != false)
            SetCharacter(characterMovementControllers[characterIndex]);
        else
        {
            characterIndex++;
            characterIndex = characterIndex % characterMovementControllers.Length;
            if (characterMovementControllers[characterIndex].enabled != false)
                SetCharacter(characterMovementControllers[characterIndex]);
            else
            {
                characterIndex -= 2;
                characterIndex = characterIndex % characterMovementControllers.Length;
            } 
        }
    }

    public void PrevCharacter()
    {
        characterIndex--;
        characterIndex = (characterIndex + characterMovementControllers.Length) % characterMovementControllers.Length;

        if (characterMovementControllers[characterIndex].enabled != false)
            SetCharacter(characterMovementControllers[characterIndex]);
        else
        {
            characterIndex--;
            characterIndex = (characterIndex + characterMovementControllers.Length) % characterMovementControllers.Length;
            if (characterMovementControllers[characterIndex].enabled != false)
                SetCharacter(characterMovementControllers[characterIndex]);
            else
            {
                characterIndex += 2;
                characterIndex = (characterIndex + characterMovementControllers.Length) % characterMovementControllers.Length;
            }
        }
    }
}