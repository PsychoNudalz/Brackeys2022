using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyAnimationPlayer : MonoBehaviour
{

    [SerializeField]
    private Animation animation;

    [SerializeField]
    private bool playOnAwake = false;

    private void Start()
    {
        if (playOnAwake)
        {
            animation.Play();
        }
    }

    public void Play(string s)
    {
        animation.Play(s);
    }
}
