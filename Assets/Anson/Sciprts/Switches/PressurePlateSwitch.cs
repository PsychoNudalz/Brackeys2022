using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSwitch : SwitchInteractable
{
    [Header("Pressure Plate")]
    [SerializeField]
    private TriggerDetector triggerZone;

    private bool wasObstructed;

    private void FixedUpdate()
    {
        if (interactState == InteractState.Off && triggerZone.IsObstructed)
        {
            OnOn();
            if (wasObstructed!=triggerZone.IsObstructed)
            {
                OnUse();
            }
        }
        else if (interactState == InteractState.On && !triggerZone.IsObstructed)
        {
            OnOff();
            if (wasObstructed!=triggerZone.IsObstructed)
            {
                OnUse();
            }
        }



        wasObstructed = triggerZone.IsObstructed;
    }
}