using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MotionBlurController : MonoBehaviour
{
    private Volume postProcess;
    private MotionBlur motionBlur;

    private void Awake()
    {
        postProcess = GetComponent<Volume>();
    }

    private void Start()
    {
        postProcess.profile.TryGet(out motionBlur);
    }

    public void motionBlurOn()
    {
        if (motionBlur != null)
        {
            motionBlur.active = true;
            StartCoroutine(turnOffBlur());
        }
    }

    private IEnumerator turnOffBlur()
    {
        yield return new WaitForSeconds(0.2f);
        motionBlurOff();
    }

    public void motionBlurOff()
    {
        if (motionBlur != null)
            motionBlur.active = false;
    }
}
