using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilityInteraction : AbilityInteraction
{
    [Header("Shield")]
    [SerializeField]
    private CharacterControllerScript characterControllerScript;

    [Header("Shield")]
    [SerializeField]
    private float shield_los_range = 50f;

    [SerializeField]
    private List<string> shield_los_tagList;

    [SerializeField]
    private LayerMask shield_los_layerMask;

    protected override void AwakeBehaviour()
    {
        base.AwakeBehaviour();
        if (!characterControllerScript)
        {
            characterControllerScript = GetComponent<CharacterControllerScript>();
        }
    }


    public override bool CanShield()
    {
        return !characterControllerScript.Equals(CharacterManager.GetMage()) && IsLineOfSight(CharacterEnum.Mage,
            shield_los_range, shield_los_tagList, shield_los_layerMask, out var raycastHit);
    }

    public override void UseShield()
    {
        CharacterManager.GetMage().CharacterAbilityHandler.UseAbility_Team(characterControllerScript);
    }
}