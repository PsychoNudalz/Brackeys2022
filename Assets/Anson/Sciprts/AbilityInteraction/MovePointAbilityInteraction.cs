using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class MovePointAbilityInteraction : AbilityInteraction
{
    [Header("Move Point")]
    [SerializeField]
    private MovePoint movePoint;

    public override bool CanMove()
    {
        return base.CanMove();
    }

    public override void UseMove()
    {
        base.UseMove();
    }
}
