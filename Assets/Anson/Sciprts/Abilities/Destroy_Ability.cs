using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Ability : Ability
{
    [Header("Destroy")]
    [SerializeField]
    private Lock_Ability lockAbility;
    public override void OnUse(object target = null)
    {
        base.OnUse(target);
    }

    public override void OnUse_Enter(object target = null)
    {
        if (target is DestructibleObject destructibleObject)
        {
            destructibleObject.Destroy();
        }
    }

    public override void OnUse_End(object target = null)
    {
        base.OnUse_End(target);
    }

    public override bool CanUse(object target = null)
    {
        if (lockAbility.AbilityState == AbilityStateEnum.Using)
        {
            return false;
        }
        if (target is DestructibleObject destructibleObject)
        {
            return true;
        }

        return false;
    }
}