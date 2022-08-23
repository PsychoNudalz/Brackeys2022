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
            foreach (ConsequenceObject consequenceObject in onOnConsequence.Concat(onOffConsequence).ToArray())
            {
                Gizmos.color = systemColour;
                
                Gizmos.DrawLine(transform.position,consequenceObject.transform.position);
            }
        }
    }

    private void Start()
    {
        SetColour();
    }

    [ContextMenu("Set Colour")]
    public void SetColour()
    {
        Material systemMaterial;
        foreach (Renderer systemRenderer in systemRenderers)
        {
            systemMaterial = systemRenderer.material;
            systemMaterial.color = systemColour;
            if (systemMaterial.HasColor(colourMaterialName))
            {
                systemMaterial.SetColor(colourMaterialName,systemColour);
            }
        }
    }
    


    [ContextMenu("Auto Find renderers in consequence")]
    public void FindRenderersInConsequences()
    {
        foreach (ConsequenceObject consequenceObject in onOnConsequence.Concat(onOffConsequence).ToArray())
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
    
    
}
