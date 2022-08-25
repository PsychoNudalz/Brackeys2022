using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollapsableFloor : MonoBehaviour
{
    [SerializeField]
    private TriggerDetector triggerDetector;

    [SerializeField]
    private bool wasObstructed;

    [SerializeField]
    private UnityEvent onCollapse;
    [SerializeField]
    private float checkTime = 1f;

    private float lastCheck;

    private void FixedUpdate()
    {
        if (Time.time - lastCheck > checkTime)
        {
            lastCheck = Time.time;
            if (triggerDetector.IsObstructed)
            {
                wasObstructed = true;
            }else if (wasObstructed)
            {
                onCollapse.Invoke();
            }
        }
    }
}
