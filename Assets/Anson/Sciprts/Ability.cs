using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AbilityEffect))]
public abstract class Ability : MonoBehaviour
{
    enum AbilityState
    {
        Off,
        Using
    }
    [Header("Base Ability")]
    [SerializeField]
    private UnityEvent onUseEvent;

    [SerializeField]
    private AbilityState abilityState = AbilityState.Off;

    [Header("Components")]
    private AbilityEffect abilityEffect;

    private void Awake()
    {
        if (!abilityEffect)
        {
            abilityEffect = GetComponent<AbilityEffect>();
        }
    }

    public virtual void OnUse()
    {
        if (CanUse())
        {
            onUseEvent.Invoke();
            abilityState = AbilityState.Using;
        }
    }

    protected virtual bool CanUse()
    {
        return true;
    }
}
