using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitch : SwitchInteractable
{
    [Header("Button")]
    [SerializeField]
    private float timer = 5f;

    [SerializeField]
    private float timer_Now = 0;


    private void Update()
    {
        if (interactState == InteractState.On)
        {
            timer_Now -= Time.deltaTime;
            if (timer_Now < 0)
            {
                OnOff();
            }
        }
    }

    public float GetTimeFloat()
    {
        return timer_Now / timer;
    }

    public override void OnUse()
    {
        OnOn();
    }

    public override void OnOn()
    {
        timer_Now = timer;
        if (interactState == InteractState.Off)
        {
            base.OnOn();
        }
    }
}