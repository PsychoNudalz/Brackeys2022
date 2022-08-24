using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("a");
    
        if (other.tag == "DamageBox")
            Debug.Log("SA");
    }
}
