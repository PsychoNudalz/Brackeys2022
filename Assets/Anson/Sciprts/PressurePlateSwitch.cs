using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSwitch : SwitchInteractable
{
    [Header("Pressure Plate")]
    [SerializeField]
    private TriggerDetector triggerZone;

    private void FixedUpdate()
    {
        if (interactState == InteractState.Off && triggerZone.IsObstructed)
        {
            OnOn();
        }
        else if (interactState == InteractState.On && !triggerZone.IsObstructed)
        {
            OnOff();
        }
    }
}