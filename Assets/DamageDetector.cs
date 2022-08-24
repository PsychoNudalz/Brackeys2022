using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    private GameManager gm;
    private CharacterControllerScript charControl;

    private void Awake()
    {
        charControl = GetComponent<CharacterControllerScript>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DamageBox")
            characterDeath();
    }

    private void characterDeath()
    {
        charControl.killCharacter();
        gm.checkForGameOver();
    }
}
