using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Character Controllers")]
    [SerializeField]
    private CharacterControllerScript soldier;

    [SerializeField]
    private CharacterControllerScript archer;

    [SerializeField]
    private CharacterControllerScript mage;

    [SerializeField]
    private CharacterControllerScript[] characterControllers;

    public CharacterControllerScript[] CharacterControllers => characterControllers;

    [Header("Settings")]
    [SerializeField]
    [Range(0f, 90f)]
    private float slantDegree = 20f;

    [Header("Components")]
    [SerializeField]
    private Transform mainCamera;


    public CharacterControllerScript Soldier => soldier;

    public CharacterControllerScript Archer => archer;

    public CharacterControllerScript Mage => mage;

    public static CharacterManager current;


    private void Awake()
    {
        mainCamera = Camera.main.transform;
        if (current)
        {
            //May cause crash if other components are up to date to the latest one
            Destroy(current);
        }

        current = this;
        SetCharacters();
    }

    [ContextMenu("Slant Character")]
    public void SlantCharacter()
    {
        foreach (CharacterControllerScript characterController in characterControllers)
        {
            characterController.SlantCharacter(slantDegree, mainCamera);
        }
    }

    [ContextMenu("Set Characters")]
    public void SetCharacters()
    {
        foreach (CharacterControllerScript characterControllerScript in
                 characterControllers)
        {
            switch (characterControllerScript.CharacterEnum)
            {
                case CharacterEnum.Soldier:
                    soldier = characterControllerScript;
                    break;
                case CharacterEnum.Archer:
                    archer = characterControllerScript;
                    break;
                case CharacterEnum.Mage:
                    mage = characterControllerScript;
                    break;
            }
        }
    }


    public static CharacterControllerScript GetSoldier()
    {
        return current.Soldier;
    }

    public static CharacterControllerScript GetArcher()
    {
        return current.Archer;
    }

    public static CharacterControllerScript GetMage()
    {
        return current.Mage;
    }

    public static CharacterControllerScript GetCharacter(CharacterEnum character)
    {
        switch (character)
        {
            case CharacterEnum.Soldier:
                return GetSoldier();
                break;
            case CharacterEnum.Archer:
                return GetArcher();
                break;
            case CharacterEnum.Mage:
                return GetMage();
                break;
        }

        Debug.LogError("Can't find character");
        return GetSoldier();
    }
}