using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleAbilityInteraction : AbilityInteraction
{
    [Header("Destructible")]
    [SerializeField]
    private float destroy_range = 3;

    [SerializeField]
    private List<string> destroy_los_tagList;

    [SerializeField]
    private LayerMask destroy_los_layerMask;

    [SerializeField]
    private DestructibleObject destructibleObject;


    //Anson: Adding movable object here is bad, but idk what to do
    [Header("Movable Object")]
    [SerializeField]
    private MovableObject movableObject;

    [SerializeField]
    private float move_select_range = 3;

    [SerializeField]
    private float move_target_range = 3;

    [SerializeField]
    private List<string> move_los_tagList;

    [SerializeField]
    private LayerMask move_los_layerMask;


    public override bool CanMove()
    {
        if (!movableObject)
        {
            return false;
        }
        
        

        return base.CanMove();
    }

    public override void UseMove()
    {
        base.UseMove();
    }

    public override bool CanDestroy()
    {
        bool b = false;
        if (IsLineOfSight(CharacterEnum.Soldier, destroy_range, destroy_los_tagList, destroy_los_layerMask,
                out var raycastHit))
        {
            if (CharacterManager.GetSoldier().CharacterAbilityHandler.CanUseAbility_Main(destructibleObject))
            {
                b = true;
            }
        }

        return b;
    }

    public override void UseDestroy()
    {
        CharacterManager.GetSoldier().CharacterAbilityHandler.UseAbility_Main(destructibleObject);
    }
}