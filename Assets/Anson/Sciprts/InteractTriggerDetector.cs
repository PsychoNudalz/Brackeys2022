using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTriggerDetector : TriggerDetector
{
    [Header("Interactable")]
    [SerializeField]
    private InteractableObject interactableObject;

    [SerializeField]
    private Collider interactableObjectCollider;

    public InteractableObject InteractableObject => interactableObject;

    // Start is called before the first frame update
    protected override void OnTriggerEnterEvent(Collider other)
    {
        base.OnTriggerEnterEvent(other);
        InteractableObject temp = other.GetComponentInParent<InteractableObject>();
        // if (!temp)
        // {
        //     temp = other.GetComponentInParent<SwitchInteractable>();
        // }
        if (temp)
        {
            interactableObject = temp;
            interactableObjectCollider = other;
        }
    }

    protected override void OnTriggerExitEvent(Collider other)
    {
        if (other.Equals(interactableObjectCollider))
        {
            interactableObject = null;
            interactableObjectCollider = null;
        }
        base.OnTriggerExitEvent(other);

    }
}
