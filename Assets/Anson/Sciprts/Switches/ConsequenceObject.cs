using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Search;

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

    [Header("Changeable materials")]
    [SerializeField]
    private Renderer[] renderers;

    public Renderer[] Renderers => renderers;

    [ContextMenu("OnUse")]
    private void Awake()
    {
        // FindRenderersInConsequences();
        if (consequenceState == ConsequenceState.Off)
        {
            OnOff();
        }
        else if (consequenceState == ConsequenceState.On)
        {
            OnOn();
        }
    }
    
    [ContextMenu("Find all renderers")]
    public void FindRenderersInConsequences()
    {
        List<Renderer> temp = new List<Renderer>(renderers);
        List<Renderer> newList = new List<Renderer>();
        foreach (ConsequenceObject consequenceObject in GetComponentsInChildren<ConsequenceObject>())
        {
            foreach (Renderer renderer in consequenceObject.Renderers)
            {
                if (!temp.Contains(renderer))
                {
                    newList.Add(renderer);
                }
            }
        }
    
        renderers = newList.ToArray();
    }

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