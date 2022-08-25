using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class MovePointAbilityInteraction : AbilityInteraction
{
    [Header("Move Point")]
    [SerializeField]
    private MovePoint movePoint;

    public MovePoint MovePoint => movePoint;
    public bool InRange => movePoint.IsInRange;

    public override bool CanMove()
    {
        if (CharacterManager.GetMage().Alive == AliveEnum.Dead)
        {
            return false;
        }
        return CharacterManager.GetMage().CharacterAbilityHandler.CanUseAbility_Main(movePoint);
    }

    public override void UseMove()
    {
        if (CanMove())
        {
            CharacterManager.GetMage().CharacterAbilityHandler.UseAbility_Main(movePoint);
        }
    }
}