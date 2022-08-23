using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock_Ability : Ability
{
    [Header("Lock")]
    [SerializeField]
    private SwitchInteractable currentSwitch;

    public override void OnUse(object target = null)
    {
        
        if (target is SwitchInteractable interactable)
        {
            if (currentSwitch)
            {
                currentSwitch.OnSwordLock(false);
            }
            currentSwitch = interactable;
            currentSwitch.OnSwordLock(true);
        }
    }

    public override void OnUse_Enter(object target = null)
    {
        base.OnUse_Enter(target);
    }

    public override void OnUse_End(object target = null)
    {
        base.OnUse_End(target);
    }

    public override bool CanUse(object target = null)
    {
        if (target is SwitchInteractable interactable)
        {
            if (interactable.InteractState_Current is InteractableObject.InteractState.On
                or InteractableObject.InteractState.Off)
            {
                return true;
            }
        }
        return false;
    }
}
