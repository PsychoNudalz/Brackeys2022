using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private PlayerUIInteractOptionController playerUIInteractOptionController;

    [SerializeField]
    private AbilityInteraction abilityInteraction;

    [SerializeField]
    private Button button;

    [SerializeField]
    private TMPro.TextMeshProUGUI text;


    public AbilityEnum AbilityEnum => abilityEnum;


    private void Awake()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }

        if (!playerUIInteractOptionController)
        {
            playerUIInteractOptionController = GetComponentInParent<PlayerUIInteractOptionController>();
        }
    }

    private void Start()
    {
        text.text = abilityEnum.ToString();
    }

    private void FixedUpdate()
    {
        //Might be expensive resource
        UpdateAvailability(abilityInteraction);
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
        button.interactable = b;
        this.abilityInteraction = abilityInteraction;
        UpdateAvailability(abilityInteraction);
    }

    private void UpdateAvailability(AbilityInteraction abilityInteraction)
    {
        if (!abilityInteraction)
        {
            return;
        }
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

    public void UseButton()
    {
        if (!playerUIInteractOptionController)
        {
            Debug.LogError(gameObject+" missing player UI Interaction Option controller");
            return;
        }
        playerUIInteractOptionController.UseButton(abilityEnum);
        playerUIInteractOptionController.SetWheelActive(false);
    }
}