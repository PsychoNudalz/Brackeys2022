using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField]
    private bool isObstructed;

    [SerializeField]
    private List<string> tagList;


    [SerializeField]
    private List<Collider> obstructedColliders = new List<Collider>();

    [Header("Cast Check")]
    [SerializeField]
    private bool useCastCheck;

    [SerializeField]
    private Vector3 centre;

    [SerializeField]
    private Vector3 size;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float checkTime = 1f;

    private float lastCheck;

    public bool IsObstructed => isObstructed;

    private void Awake()
    {
    }

    private void FixedUpdate()
    {
        if (useCastCheck)
        {
            if (Time.time - lastCheck > checkTime)
            {
                lastCheck = Time.time;
                List<Collider> colliders = new List<Collider>();
                foreach (RaycastHit raycastHit in Physics.BoxCastAll(transform.position + centre,
                             new Vector3(size.x / 2f, size.y / 2f, size.z / 2f), transform.up, transform.rotation,
                             size.magnitude, layerMask))
                {
                    colliders.Add(raycastHit.collider);
                }

                foreach (Collider c in colliders)
                {
                    if (!obstructedColliders.Contains(c))
                    {
                        OnTriggerEnterEvent(c);
                    }
                }


                foreach (Collider c in new List<Collider>(obstructedColliders))
                {
                    if (!colliders.Contains(c))
                    {
                        OnTriggerExitEvent(c);
                    }
                }

                obstructedColliders = colliders;
                isObstructed = obstructedColliders.Count > 0;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent(other);
    }

    protected virtual void OnTriggerEnterEvent(Collider other)
    {
        if (tagList.Count == 0 || tagList.Contains(other.tag))
        {
            if (!obstructedColliders.Contains(other))
            {
                obstructedColliders.Add(other);
            }

            isObstructed = obstructedColliders.Count > 0;
        }
    }

    protected virtual void OnTriggerExitEvent(Collider other)
    {
        if (tagList.Count == 0 || tagList.Contains(other.tag))
        {
            if (obstructedColliders.Contains(other))
            {
                obstructedColliders.Remove(other);
            }

            isObstructed = obstructedColliders.Count > 0;
        }
    }
}