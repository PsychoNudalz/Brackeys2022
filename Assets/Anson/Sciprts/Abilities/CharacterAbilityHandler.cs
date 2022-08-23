using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilityHandler : MonoBehaviour
{
    [Header("Abilities")]
    [SerializeField]
    private Ability ability_main;

    [SerializeField]
    private Ability ability_team;

    public Ability AbilityMain => ability_main;

    public Ability AbilityTeam => ability_team;

    public bool CanUseAbility_Main(object target = null)
    {
        if (!ability_main)
        {
            return false;
        }
        return ability_main.CanUse(target);
    }

    public void UseAbility_Main(object target = null)
    {
        if (!ability_main)
        {
            return ;
        }
        ability_main.OnUse(target);
    }

    public bool CanUseAbility_Team(object target = null)
    {
        if (!ability_team)
        {
            return false;
        }
        return ability_team.CanUse(target);
    }

    public void UseAbility_Team(object target = null)
    {
        if (!ability_team)
        {
            return;
        }
        ability_team.OnUse(target);
    }
}