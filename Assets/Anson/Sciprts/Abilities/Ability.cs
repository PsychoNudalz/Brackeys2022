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
    protected UnityEvent onUseEndEvent;

    [SerializeField]
    protected AbilityStateEnum abilityStateEnum = AbilityStateEnum.Off;

    [Header("Components")]
    protected AbilityEffect abilityEffect;

    public AbilityStateEnum AbilityState => abilityStateEnum;

    private void Awake()
    {
        if (!abilityEffect)
        {
            abilityEffect = GetComponent<AbilityEffect>();
        }
    }

    public virtual void OnUse(object target = null)
    {
        OnUse_Enter(target);
        OnUse_End(target);
    }

    public virtual void OnUse_Enter(object target = null)
    {
        onUseEnterEvent.Invoke();
        abilityStateEnum = AbilityStateEnum.Using;
    }

    public virtual void OnUse_End(object target = null)
    {
        onUseEndEvent.Invoke();
        abilityStateEnum = AbilityStateEnum.Off;
    }

    public virtual bool CanUse(object target = null)
    {
        return abilityStateEnum == AbilityStateEnum.Using;
    }
}