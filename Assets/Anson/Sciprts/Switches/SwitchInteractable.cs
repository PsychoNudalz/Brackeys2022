using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwitchInteractable : InteractableObject
{
    [Header("Switch Settings")]
    [SerializeField]
    private Color systemColour;

    [SerializeField]
    private List<Renderer> systemRenderers;

    private string colourMaterialName = "_MainColor";
    [Header("Debug")]
    [SerializeField]
    private bool showDebug = true;

    private void OnDrawGizmosSelected()
    {
        if (showDebug)
        {
            foreach (ConsequenceObject consequenceObject in AllConsequenceObjects())
            {
                Gizmos.color = systemColour;
                try
                {
                    Gizmos.DrawLine(transform.position, consequenceObject.transform.position);
                }
                catch (NullReferenceException e)
                {
                    
                }
            }
        }
    }

    private ConsequenceObject[] AllConsequenceObjects()
    {
        return onUseConsequence.Concat(onOnConsequence.Concat(onOffConsequence)).ToArray();
    }

    private void Start()
    {
        FindRenderersInConsequences();
        SetColour();
        if (interactState == InteractState.On)
        {
            OnOn();
        }else if (interactState == InteractState.Off)
        {
            OnOff();
        }
    }

    [ContextMenu("Set Colour")]
    public void SetColour()
    {
        Material systemMaterial;
        foreach (Renderer systemRenderer in systemRenderers)
        {
            if (systemRenderer)
            {
                systemMaterial = systemRenderer.material;
                systemMaterial.color = systemColour;
                if (systemMaterial.HasColor(colourMaterialName))
                {
                    systemMaterial.SetColor(colourMaterialName,systemColour);
                }
            }
        }
    }
    


    [ContextMenu("Auto Find renderers in consequence")]
    public void AutoFindRenderersInConsequences()
    {
        foreach (ConsequenceObject consequenceObject in AllConsequenceObjects())
        {
            foreach (Renderer renderer in consequenceObject.GetComponentsInChildren<Renderer>())
            {
                if (!systemRenderers.Contains(renderer))
                {
                    systemRenderers.Add(renderer);
                }
            }
        }
    }
    [ContextMenu("Find renderers in consequence")]
    public void FindRenderersInConsequences()
    {
        foreach (ConsequenceObject consequenceObject in AllConsequenceObjects())
        {
            foreach (Renderer renderer in consequenceObject.Renderers)
            {
                if (!systemRenderers.Contains(renderer))
                {
                    systemRenderers.Add(renderer);
                }
            }
        }
    }
    
    
}
