using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Ability : Ability
{
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

        if (target is DestructibleObject destructibleObject)
        {
            return true;
        }

        return false;
    }
}