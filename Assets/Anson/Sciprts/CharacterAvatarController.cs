using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAvatarController : MonoBehaviour
{
    [SerializeField]
    private Animator avatar;

    private void Awake()
    {
        if (!avatar)
        {
            avatar = GetComponentInChildren<Animator>();
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
}
