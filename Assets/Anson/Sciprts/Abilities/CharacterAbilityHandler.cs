using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilityHandler : MonoBehaviour
{
    [SerializeField]
    private CharacterControllerScript characterControllerScript;

    [SerializeField]
    private CharacterEffectsController characterEffectsController;
    
    [Header("Abilities")]
    [SerializeField]
    private Ability ability_main;

    [SerializeField]
    private Ability ability_team;

    public Ability AbilityMain => ability_main;

    public Ability AbilityTeam => ability_team;

    public CharacterControllerScript CharacterControllerScript
    {
        get => characterControllerScript;
        set => characterControllerScript = value;
    }

    public bool CanUseAbility_Main(object target = null)
    {
        if (!ability_main||characterControllerScript.Alive == AliveEnum.Dead)
        {
            return false;
        }
        return ability_main.CanUse(target);
    }

    public void UseAbility_Main(object target = null)
    {
        if (!ability_main||characterControllerScript.Alive == AliveEnum.Dead)
        {
            return ;
        }
        characterEffectsController?.Ability_Main();
        ability_main.OnUse(target);
    }

    public bool CanUseAbility_Team(object target = null)
    {
        if (!ability_team||characterControllerScript.Alive == AliveEnum.Dead)
        {
            return false;
        }
        return ability_team.CanUse(target);
    }

    public void UseAbility_Team(object target = null)
    {
        if (!ability_team||characterControllerScript.Alive == AliveEnum.Dead)
        {
            return;
        }
        characterEffectsController?.Ability_Team();
        ability_team.OnUse(target);
    }
}