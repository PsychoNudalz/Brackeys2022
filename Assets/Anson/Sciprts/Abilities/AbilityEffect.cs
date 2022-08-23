using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityEffect : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onUseStartEvent;

    [SerializeField]
    private UnityEvent onUseEndEvent;


    public void OnUse_Start()
    {
        onUseEndEvent.Invoke();
    }

    public void OnUse_End()
    {
        onUseEndEvent.Invoke();
    }
}