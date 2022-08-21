using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Character Controllers")]
    [SerializeField]
    private CharacterControllerScript[] characterControllers;

    public CharacterControllerScript[] CharacterControllers => characterControllers;

    [Header("Settings")]
    [SerializeField]
    [Range(0f,90f)]
    private float slantDegree = 20f;
    
    [Header("Components")]
    [SerializeField]
    private Transform mainCamera;
    
    private void Awake()
    {
        mainCamera = Camera.main.transform;
        
    }

    [ContextMenu("Slant Character")]
    public void SlantCharacter()
    {
        foreach (CharacterControllerScript characterController in characterControllers)
        {
            characterController.SlantCharacter(slantDegree,mainCamera);
        }
    }
}
