using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractOptionButtonController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AbilityEnum abilityEnum;

    [SerializeField]
    private bool available = true;

    [Header("Components")]
    [SerializeField]
    GameObject unavailableSprite;


    public AbilityEnum AbilityEnum => abilityEnum;


    private void Awake()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void HoverEnter()
    {
        animator.SetBool("Hover", true);
    }

    public void HoverExit()
    {
        animator.SetBool("Hover", false);
    }

    public override bool Equals(object other)
    {
        if (other is AbilityEnum @enum)
        {
            return abilityEnum.Equals(@enum);
        }

        return base.Equals(other);
    }

    public void SetActive(bool b, AbilityInteraction abilityInteraction)
    {
        gameObject.SetActive(b);
        switch (abilityEnum)
        {
            case AbilityEnum.Destroy:
                available = abilityInteraction.CanDestroy();
                break;
            case AbilityEnum.SwordLock:
                available = abilityInteraction.CanSwordLock();

                break;
            case AbilityEnum.Shoot:
                available = abilityInteraction.CanShoot();

                break;
            case AbilityEnum.BoostUp:
                available = abilityInteraction.CanBoostUp();

                break;
            case AbilityEnum.Move:
                available = abilityInteraction.CanMove();

                break;
            case AbilityEnum.Shield:
                available = abilityInteraction.CanShield();

                break;
        }

        unavailableSprite.SetActive(!available);
    }
}