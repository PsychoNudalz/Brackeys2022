using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private CanvasGroup canvas;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
            LeanTween.alphaCanvas(canvas, 1, 0.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character")
            LeanTween.alphaCanvas(canvas, 0, 0.5f);
    }
}
