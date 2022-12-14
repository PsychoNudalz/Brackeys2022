using System;
using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HighlightEffect))]

public class ClickableObject : MonoBehaviour
{
    
    [Header("Behaviour")]
    [SerializeField]
    private UnityEvent onClickEvent;

    [SerializeField]
    private AbilityInteraction abilityInteraction;
    
    [Space(10f)]
    [SerializeField]
    private HighlightEffect highlightEffect;
    

    private bool isSelected = false;

    private void Awake()
    {
        if (!highlightEffect)
        {
            highlightEffect = GetComponent<HighlightEffect>();
        }
    }

    public void OnClick()
    {
        onClickEvent.Invoke();
    }

    /// <summary>
    /// When clickable object is being hovered
    /// </summary>
    /// <param name="b"></param>
    public void OnHover(bool b)
    {
        if (b)
        {
            highlightEffect.highlighted = true;
        }
        else if(!isSelected)
        {
            highlightEffect.highlighted = false;
        }
    }
    
    /// <summary>
    /// When clickable object is being selected
    /// </summary>
    /// <param name="b"></param>
    public void OnSelect(bool b)
    {
        isSelected = b;
        if (b)
        {
            if (abilityInteraction)
            {
                ShowInteractionWheel();
            }
            OnHover(true);
            highlightEffect.glow = 1;
            OnClick();
        }
        else
        {
            // if (abilityInteraction)
            // {
            //     HideInteractionWheel();
            // }
            OnHover(false);
            highlightEffect.glow = 0;
        }
    }

    public void ShowInteractionWheel()
    {
        abilityInteraction.ShowInteractionWheel();
    }
    
    public void HideInteractionWheel()
    {
        abilityInteraction.HideInteractionWheel();
    }
}
