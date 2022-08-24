using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableObject : MonoBehaviour
{
    public enum InteractState
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

    [Header("Locks")]
    [SerializeField]
    private UnityEvent OnSword_Lock;

    [SerializeField]
    private UnityEvent OnSword_Unlock;

    [SerializeField]
    private UnityEvent OnArrow_Lock;

    [SerializeField]
    private UnityEvent OnArrow_Unlock;


    public InteractState InteractState_Current => interactState;


    [ContextMenu("OnUse")]
    public virtual bool OnUse()
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

            return true;
        }

        return false;
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
    public virtual void SetLock(bool forcedType, bool b)
    {
        if (interactState is InteractState.On or InteractState.Off)
        {
            originalInteractState = interactState;
        }

        if (b)
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

        //Debugging
        print(gameObject.name + " Set state to: " + interactState.ToString());
    }

    public virtual void LockCurrent(bool b)
    {
        if (b)
        {
            if (interactState == InteractState.On)
            {
                SetLock(true, true);
            }
            else if (interactState == InteractState.Off)
            {
                SetLock(false, true);
            }
        }
        else
        {
            if (interactState is InteractState.LOCK_ON or InteractState.LOCK_OFF)
            {
                SetLock(true, false);
            }
        }
    }

    public virtual void OnSwordLock(bool b)
    {
        LockCurrent(b);
        if (b)
        {
            OnSword_Lock.Invoke();
        }
        else
        {
            OnSword_Unlock.Invoke();
        }
    }

    public virtual void OnArrowLock(bool b)
    {
        LockCurrent(b);
        if (b)
        {
            OnArrow_Lock.Invoke();
        }
        else
        {
            OnArrow_Unlock.Invoke();
        }
    }
}