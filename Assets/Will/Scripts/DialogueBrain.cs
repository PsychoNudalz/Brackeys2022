using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBrain : MonoBehaviour
{
    [SerializeField] private SpeechController speechCtrl;
    [SerializeField] private int characterIndex;
    public string[] newString;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            StartCoroutine(speechCtrl.talk(newString, characterIndex));
            GetComponent<Collider>().enabled = false;
        }
    }
}
