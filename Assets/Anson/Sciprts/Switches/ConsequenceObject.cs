using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConsequenceObject : MonoBehaviour
{
    enum ConsequenceState
    {
        Off,
        On
    }


    [Header("Base Interaction")]
    [SerializeField]
    private UnityEvent onOnEvent;

    [SerializeField]
    private UnityEvent onOffEvent;


    [SerializeField]
    private ConsequenceState consequenceState = ConsequenceState.Off;

    [ContextMenu("OnUse")]
    public virtual void OnUse()
    {
        if (CanUse())
        {
            if (consequenceState == ConsequenceState.Off)
            {
                OnOn();
            }
            else if (consequenceState == ConsequenceState.On)
            {
                OnOff();
            }
        }
    }

    public virtual void OnOff()
    {
        onOffEvent.Invoke();
        consequenceState = ConsequenceState.Off;
    }

    public virtual void OnOn()
    {
        onOnEvent.Invoke();
        consequenceState = ConsequenceState.On;
    }


    protected virtual bool CanUse()
    {
        return true;
    }
}