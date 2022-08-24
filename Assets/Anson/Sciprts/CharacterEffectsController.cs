using System;
using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using UnityEngine;

public class CharacterEffectsController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField]
    private Animator avatar;
    [Space(10)]
    [Header("Highlight")]
    [SerializeField]
    private HighlightEffect selectHighlightEffect;

    [SerializeField]
    private int numberOfFlash = 3;

    private Coroutine flashCoroutine;

    public HighlightEffect SelectHighlightEffect => selectHighlightEffect;

    private void Awake()
    {
        if (!avatar)
        {
            avatar = GetComponentInChildren<Animator>();
        }

        if (!selectHighlightEffect)
        {
            selectHighlightEffect = GetComponentInChildren<HighlightEffect>();
        }
    }

    public void SetWalk(float f)
    {
        avatar.SetFloat("WalkSpeed",f);
    }

    public void Interact()
    {
        avatar.SetTrigger("Interact");
    }

    public void Climb()
    {
        avatar.SetTrigger("Climb");
    }

    public void SetModelSelectHighlight(bool b)
    {
        if (b)
        {
            flashCoroutine= StartCoroutine(SelectCharacterHighlightAnimation());
        }
        else
        {
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
            }
            selectHighlightEffect.highlighted = false;
        }
    }
    private IEnumerator SelectCharacterHighlightAnimation()
    {
        bool b = true;
        for (int i = 0; i < numberOfFlash*2; i++)
        {
            selectHighlightEffect.highlighted = b;
            yield return new WaitForSeconds(0.25f);
            b = !b;
        }
        
    }
}
