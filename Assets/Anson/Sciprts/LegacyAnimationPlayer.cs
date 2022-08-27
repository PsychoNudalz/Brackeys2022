using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyAnimationPlayer : MonoBehaviour
{

    [SerializeField]
    private Animation animation;

    public void Play(string s)
    {
        animation.Play(s);
    }
}
