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

    public bool CanUseAbility_Main()
    {
        return ability_main.CanUse();
    }

    public void UseAbility_Main()
    {
        ability_main.OnUse();
    }

    public bool CanUseAbility_Team()
    {
        return ability_team.CanUse();
    }

    public void UseAbility_Team()
    {
        ability_team.OnUse();
    }
}