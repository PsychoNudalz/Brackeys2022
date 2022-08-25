using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Ability : Ability
{
    [Header("Shield")]
    [SerializeField]
    private CharacterControllerScript currentCharacter;
    public override void OnUse(object target = null)
    {

        if (target is CharacterControllerScript character)
        {
            CharacterControllerScript previousCharacter = currentCharacter;
            if (currentCharacter)
            {
                OnUse_End(currentCharacter);
            }

            if (!character.Equals(previousCharacter))
            {
                OnUse_Enter(character);
            }
        }
    }

    public override void OnUse_Enter(object target = null)
    {
        if (target is CharacterControllerScript character)
        {
            currentCharacter = character;
            currentCharacter.SetShield(true);
            base.OnUse_Enter(target);
        }
    }

    public override void OnUse_End(object target = null)
    {
        if (target is CharacterControllerScript character)
        {
            currentCharacter.SetShield(false);
            currentCharacter = null;
            base.OnUse_End(target);
        }
    }

    public override bool CanUse(object target = null)
    {
        return true;
    }
}
