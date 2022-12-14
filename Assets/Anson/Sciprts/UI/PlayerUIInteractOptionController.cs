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

    [SerializeField]
    private Vector3 worldPos;

    [SerializeField]
    private Vector2 margin = new Vector2(.1f, .1f);

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

        SetWheelActive(false, new Vector3());
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
        SetWheelActive(b, new Vector3());
    }

    public void SetWheelActive(bool b, Vector3 pos)
    {
        if (b)
        {
            if (!worldPos.Equals(pos))
            {
                SetScreenPosition(pos);
            }

            animator.SetBool("OpenWheel", true);
        }
        else
        {
            animator.SetBool("OpenWheel", false);
        }

        isActive = b;
    }

    private Vector3 SetScreenPosition(Vector3 pos)
    {
        worldPos = pos;
        Vector2 temp = Camera.main.WorldToScreenPoint(pos);
        transform.position = new Vector2(Mathf.Clamp(temp.x, Screen.width * margin.x, Screen.width * (1f - margin.x)),
            Mathf.Clamp(temp.y, Screen.height * margin.y, Screen.height * (1f - margin.y)));
        return transform.position;
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
        if (gameObject.activeSelf && isActive)
        {
            StartCoroutine(DelayPlayerClick());
            return;
        }

        // print("Activating interaction Wheel");
        gameObject.SetActive(true);
        SetWheelActive(true, pos);
        this.abilityInteraction = abilityInteraction;
        foreach (PlayerInteractOptionButtonController button in buttons)
        {
            if (abilityEnums.Contains(button.AbilityEnum))
            {
                // print("Showing: " + button.AbilityEnum);
                button.SetActive(true, abilityInteraction);
            }
            else
            {
                button.SetActive(false, abilityInteraction);
            }
        }
    }

    IEnumerator DelayPlayerClick()
    {
        yield return new WaitForEndOfFrame();
        SetWheelActive(false, new Vector3());
    }

    public void UseButton(AbilityEnum abilityEnum)
    {
        switch (abilityEnum)
        {
            case AbilityEnum.Destroy:
                UseDestroy();
                break;
            case AbilityEnum.SwordLock:
                UseLock();
                break;
            case AbilityEnum.Shoot:
                UseShoot();
                break;
            case AbilityEnum.BoostUp:
                UseBoost();
                break;
            case AbilityEnum.Move:
                UseMove();
                break;
            case AbilityEnum.Shield:
                UseShield();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(abilityEnum), abilityEnum, null);
        }
    }

    public void UseDestroy()
    {
        abilityInteraction.UseDestroy();
    }

    public void UseLock()
    {
        abilityInteraction.UseLock();
    }

    public void UseShoot()
    {
        abilityInteraction.UseShoot();
    }

    public void UseBoost()
    {
        abilityInteraction.UseBoost();
    }

    public void UseMove()
    {
        abilityInteraction.UseMove();
    }

    public void UseShield()
    {
        abilityInteraction.UseShield();
    }
}