using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbilityInteraction : MonoBehaviour
{
    [Header("Interactions")]
    [SerializeField]
    private AbilityEnum[] abilityEnums;

    [Header("Settings")]
    [SerializeField]
    private Transform centreOfMass;


    private void Awake()
    {
        AwakeBehaviour();
    }

    protected virtual void AwakeBehaviour()
    {
        if (!centreOfMass)
        {
            centreOfMass = transform;
        }
    }

    protected virtual bool IsCharacterInRange(CharacterEnum character, float range)
    {
        return range >= Vector3.Distance(centreOfMass.position,
            CharacterManager.GetCharacter(character).GetCentrePosition());
    }


    /// <summary>
    /// check line of sight
    /// right now characters break line of sight
    /// </summary>
    /// <param name="character"></param>
    /// <param name="range"></param>
    /// <param name="tagList"></param>
    /// <param name="layerMask"></param>
    /// <param name="raycastHit"></param>
    /// <returns></returns>
    protected virtual bool IsLineOfSight(CharacterEnum character, float range, List<string> tagList,
        LayerMask layerMask, out RaycastHit raycastHit)
    {
        Vector3 dir = (CharacterManager.GetCharacter(character).GetCentrePosition() - centreOfMass.position).normalized;
        if (Physics.Raycast(centreOfMass.position,
                dir,
                out raycastHit, range, layerMask))
        {
            if (tagList.Contains(raycastHit.collider.tag))
            {
                CharacterControllerScript temp = raycastHit.collider.GetComponentInParent<CharacterControllerScript>();
                if (temp && temp.CharacterEnum.Equals(character))
                {
                    Debug.DrawRay(centreOfMass.position, dir * range, Color.green, 5f);

                    return true;
                }
            }
        }

        Debug.DrawRay(centreOfMass.position, dir * range, Color.red, 5f);

        return false;
    }


    public virtual bool CanDestroy()
    {
        return false;
    }

    public virtual bool CanSwordLock()
    {
        return false;
    }

    public virtual bool CanShoot()
    {
        return false;
    }

    public virtual bool CanBoostUp()
    {
        return true;
    }

    public virtual bool CanMove()
    {
        return false;
    }

    public virtual bool CanShield()
    {
        return false;
    }

    public virtual void UseDestroy()
    {
    }

    public virtual void UseLock()
    {
    }

    public virtual void UseShoot()
    {
    }

    public virtual void UseBoost()
    {
    }

    public virtual void UseMove()
    {
    }

    public virtual void UseShield()
    {
    }

    public virtual void ShowInteractionWheel()
    {
        PlayerUIController.current.PlayerUIInteractOptionController.ActivateWheel(transform.position,
            new List<AbilityEnum>(abilityEnums), this);
    }

    public virtual void HideInteractionWheel()
    {
        PlayerUIController.current.PlayerUIInteractOptionController.SetWheelActive(false);
    }
}