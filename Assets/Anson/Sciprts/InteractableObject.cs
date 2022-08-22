using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableObject : MonoBehaviour
{
    enum InteractState
    {
        Off,
        On
    }

    [Header("Base Interaction")]
    [SerializeField]
    private UnityEvent onOnEvent;

    [SerializeField]
    private ConsequenceObject[] onOnConsequence;

    [Space(10)]
    [SerializeField]
    private UnityEvent onOffEvent;

    [SerializeField]
    private ConsequenceObject[] onOffConsequence;

    [SerializeField]
    private InteractState interactState = InteractState.Off;

    [ContextMenu("OnUse")]
    public virtual void OnUse()
    {
        if (CanUse())
        {
            if (interactState == InteractState.Off)
            {
                onOnEvent.Invoke();
                foreach (ConsequenceObject consequenceObject in onOnConsequence)
                {
                    consequenceObject.OnOn();
                }
                interactState = InteractState.On;
            }
            else if (interactState == InteractState.On)
            {
                onOffEvent.Invoke();
                foreach (ConsequenceObject consequenceObject in onOnConsequence)
                {
                    consequenceObject.OnOff();
                }
                interactState = InteractState.Off;
            }
        }
    }

    protected virtual bool CanUse()
    {
        return true;
    }
}