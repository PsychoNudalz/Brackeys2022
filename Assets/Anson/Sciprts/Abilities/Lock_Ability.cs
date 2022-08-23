using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock_Ability : Ability
{
    [Header("Lock")]
    [SerializeField]
    private SwitchInteractable currentSwitch;

    public SwitchInteractable CurrentSwitch => currentSwitch;

    public override void OnUse(object target = null)
    {
        if (abilityStateEnum == AbilityStateEnum.Using)
        {
            OnUse_End(currentSwitch);
        }

        OnUse_Enter(target);
    }

    /// <summary>
    /// Locked currentSwitch
    /// </summary>
    /// <param name="target"></param>
    public override void OnUse_Enter(object target = null)
    {
        if (target is SwitchInteractable interactable)
        {
            if (!currentSwitch || !currentSwitch.Equals(target as SwitchInteractable))
            {
                currentSwitch = interactable;
                currentSwitch.OnSwordLock(true);
                base.OnUse_Enter(target);
            }
            else
            {
                currentSwitch = null;
            }
        }
    }

    public override void OnUse_End(object target = null)
    {
        if (target is SwitchInteractable interactable)
        {
            if (currentSwitch)
            {
                currentSwitch.OnSwordLock(false);
            }
        }

        base.OnUse_End(target);
    }

    public override bool CanUse(object target = null)
    {
        if (target is SwitchInteractable interactable)
        {
            if (interactable.Equals(currentSwitch))
            {
                return true;
            }

            if (interactable.InteractState_Current is InteractableObject.InteractState.On
                or InteractableObject.InteractState.Off)
            {
                return true;
            }
        }

        return false;
    }
}