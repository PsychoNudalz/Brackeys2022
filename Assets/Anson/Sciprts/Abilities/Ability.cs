using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum AbilityEnum
{
    Destroy,
    SwordLock,
    Shoot,
    BoostUp,
    Move,
    Shield
}

[RequireComponent(typeof(AbilityEffect))]
public abstract class Ability : MonoBehaviour
{
    public enum AbilityStateEnum
    {
        Off,
        Using
    }

    [Header("Base Ability")]
    [SerializeField]
    protected UnityEvent onUseEnterEvent;

    [SerializeField]
    protected AbilityStateEnum abilityStateEnum = AbilityStateEnum.Off;

    [Header("Components")]
    protected AbilityEffect abilityEffect;

    private void Awake()
    {
        if (!abilityEffect)
        {
            abilityEffect = GetComponent<AbilityEffect>();
        }
    }

    public virtual void OnUse(object target = null)
    {
        if (CanUse())
        {
            OnUse_Enter();
        }
    }

    public virtual void OnUse_Enter(object target = null)
    {
        onUseEnterEvent.Invoke();
        abilityStateEnum = AbilityStateEnum.Using;
    }

    public virtual void OnUse_End(object target = null)
    {
        abilityStateEnum = AbilityStateEnum.Off;
    }

    public virtual bool CanUse(object target = null)
    {
        return abilityStateEnum == AbilityStateEnum.Using;
    }
}