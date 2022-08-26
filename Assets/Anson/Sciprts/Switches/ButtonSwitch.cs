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

    [SerializeField]
    private Renderer timerMaterial;


    private void Start()
    {
        FindRenderersInConsequences();
        SetColour();
        SetTimeMaterial();
    }

    private void SetTimeMaterial()
    {
        if (timerMaterial)
        {
            timerMaterial.material.SetFloat("_Value", GetTimeFloat());
        }
    }

    private void Update()
    {
        if (interactState == InteractState.On)
        {
            timer_Now -= Time.deltaTime;
            if (timer_Now < 0)
            {
                OnOff();
            }

            SetTimeMaterial();
        }
    }

    public float GetTimeFloat()
    {
        return timer_Now / timer;
    }

    public override bool OnUse()
    {
        OnOn();
        return true;
    }

    public override void OnOn()
    {
        timer_Now = timer;
        SetTimeMaterial();

        if (interactState == InteractState.Off)
        {
            base.OnOn();
            OnUseBehaviour();

        }
    }

    public override void OnOff()
    {
        OnUseBehaviour();
        base.OnOff();
    }
}