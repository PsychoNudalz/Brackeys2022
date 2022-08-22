using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableObject : MonoBehaviour
{
    protected enum InteractState
    {
        Off,
        On,
        LOCK_ON,
        LOCK_OFF
    }

    [Header("Base Interaction")]
    [SerializeField]
    protected UnityEvent onOnEvent;

    [SerializeField]
    protected ConsequenceObject[] onOnConsequence;

    [Space(10)]
    [SerializeField]
    protected UnityEvent onOffEvent;

    [SerializeField]
    protected ConsequenceObject[] onOffConsequence;
    
    [SerializeField]
    protected InteractState interactState = InteractState.Off;
    protected InteractState originalInteractState = InteractState.Off;

    [Header("Debug")]
    [SerializeField]
    private bool showDebug = false;
    

    [ContextMenu("OnUse")]
    public virtual void OnUse()
    {
        if (CanUse())
        {
            if (interactState == InteractState.Off)
            {
                OnOn();
            }
            else if (interactState == InteractState.On)
            {
                OnOff();
            }
        }
    }

    public virtual void OnOff()
    {
        if (!CanUse())
        {
            return;
        }
        onOffEvent.Invoke();
        foreach (ConsequenceObject consequenceObject in onOnConsequence)
        {
            consequenceObject.OnOff();
        }

        interactState = InteractState.Off;
    }

    public virtual void OnOn()
    {
        if (!CanUse())
        {
            return;
        }
        onOnEvent.Invoke();
        foreach (ConsequenceObject consequenceObject in onOnConsequence)
        {
            consequenceObject.OnOn();
        }

        interactState = InteractState.On;
    }

    protected virtual bool CanUse()
    {
        return true;
    }

    /// <summary>
    /// Set the lock of the of the intersection to force it on or off
    /// </summary>
    /// <param name="forcedType">Which type</param>
    /// <param name="b">on or off</param>
    protected virtual void SetLock(bool forcedType, bool b)
    {
        if (interactState is InteractState.On or InteractState.Off)
        {
            originalInteractState = interactState;
        }
        if (!b)
        {
            //turning off a lock
            if (forcedType)
            {
                interactState = InteractState.LOCK_ON;
            }
            else
            {
                interactState = InteractState.LOCK_OFF;
            }
        }
        else
        {
            interactState = originalInteractState;
        }
    }
}