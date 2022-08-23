using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIInteractOptionController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private bool isActive;

    public bool IsActive => isActive;

    [SerializeField]
    private PlayerInteractOptionButtonController[] buttons;

    [SerializeField]
    private AbilityInteraction abilityInteraction;

    private void Awake()
    {
        if (!abilityInteraction)
        {
            abilityInteraction = GetComponent<AbilityInteraction>();
        }

        if (!animator)
        {
            animator = GetComponent<Animator>();
        }

        SetWheelActive(false);
        if (buttons.Length == 0)
        {
            FindAllButtons();
        }
    }

    [ContextMenu("Find all button")]
    private void FindAllButtons()
    {
        buttons = GetComponentsInChildren<PlayerInteractOptionButtonController>();
    }

    public void SetWheelActiveEvent(bool b)
    {
        SetWheelActive(b);
    }

    public void SetWheelActive(bool b, Vector3 pos = new Vector3())
    {
        if (b)
        {
            animator.SetBool("OpenWheel", true);
        }
        else
        {
            animator.SetBool("OpenWheel", false);
        }

        isActive = b;
        if (pos.magnitude > .001f)
        {
            transform.position = Camera.main.WorldToScreenPoint(pos);
        }
    }

    /// <summary>
    /// Animation only
    /// </summary>
    public void DisableWheel()
    {
        gameObject.SetActive(false);
    }

    public void ActivateWheel(Vector3 pos, List<AbilityEnum> abilityEnums, AbilityInteraction abilityInteraction)
    {
        print("Activating interaction Wheel");
        gameObject.SetActive(true);
        SetWheelActive(true, pos);
        this.abilityInteraction = abilityInteraction;
        foreach (PlayerInteractOptionButtonController button in buttons)
        {
            if (abilityEnums.Contains(button.AbilityEnum))
            {
                print("Showing: " + button.AbilityEnum);
                button.SetActive(true, abilityInteraction);
            }
            else
            {
                button.SetActive(false, abilityInteraction);
            }
        }
    }
}