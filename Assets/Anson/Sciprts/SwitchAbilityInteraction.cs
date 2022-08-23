using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAbilityInteraction : AbilityInteraction
{
    [Header("Switch Ability Interaction")]
    [Header("Lock")]
    [SerializeField]
    private float lock_range = 3;

    [SerializeField]
    private List<string> lock_los_tagList;

    [SerializeField]
    private LayerMask lock_los_layerMask;

    [Header("Shoot")]
    [SerializeField]
    private float shoot_los_range = 50f;

    [SerializeField]
    private List<string> shoot_los_tagList;

    [SerializeField]
    private LayerMask shoot_los_layerMask;

    [Header("Move")]
    [SerializeField]
    private float move_range = 3;

    [SerializeField]
    private List<string> move_los_tagList;

    [SerializeField]
    private LayerMask move_los_layerMask;

    [Space(10f)]
    [Header("Components")]
    [SerializeField]
    private SwitchInteractable switchInteractable;


    protected override void AwakeBehaviour()
    {
        base.AwakeBehaviour();
        if (!switchInteractable)
        {
            switchInteractable = GetComponent<SwitchInteractable>();
        }
    }

    public override bool CanDestroy()
    {
        return base.CanDestroy();
    }

    public override bool CanSwordLock()
    {
        bool b = false;
        if (IsLineOfSight(CharacterEnum.Soldier, lock_range, lock_los_tagList, lock_los_layerMask,
                out var raycastHit))
        {
            if (CharacterManager.GetSoldier().CharacterAbilityHandler.CanUseAbility_Team(switchInteractable))
            {
                b = true;
            }
        }

        return b;
    }

    public override bool CanShoot()
    {
        return base.CanShoot();
    }

    public override bool CanBoostUp()
    {
        return base.CanBoostUp();
    }

    public override bool CanMove()
    {
        return base.CanMove();
    }

    public override bool CanShield()
    {
        return base.CanShield();
    }

    public override void UseDestroy()
    {
        base.UseDestroy();
    }

    public override void UseLock()
    {
        
        CharacterManager.GetSoldier().CharacterAbilityHandler.UseAbility_Team(switchInteractable);
    }

    public override void UseShoot()
    {
        base.UseShoot();
    }

    public override void UseBoost()
    {
        base.UseBoost();
    }

    public override void UseMove()
    {
        base.UseMove();
    }

    public override void UseShield()
    {
        base.UseShield();
    }
}