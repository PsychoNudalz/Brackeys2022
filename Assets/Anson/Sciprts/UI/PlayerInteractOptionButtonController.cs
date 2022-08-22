using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractOptionButtonController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void HoverEnter()
    {
        animator.SetBool("Hover",true);
    }

    public void HoverExit()
    {
        animator.SetBool("Hover", false);
    }
}
