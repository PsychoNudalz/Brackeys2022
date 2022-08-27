using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    private GameManager gm;
    private CharacterControllerScript charControl;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "DamageBox")
            killCharacter(other.gameObject);
        if (gameObject.tag == "Escape")
            gm.loadNextLevel();
    }

    private void killCharacter(GameObject character)
    {
        charControl = character.GetComponent<CharacterControllerScript>();
        if (charControl)
        {
            charControl.KillCharacter();
        }
    }
}
