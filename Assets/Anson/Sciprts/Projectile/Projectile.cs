using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.5f;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float lifetime = 20f;

    private float spawnTime;

    [Header("Deflect")]
    [SerializeField]
    private string deflectTag = "Deflect";

    [SerializeField]
    private LayerMask deflectLayerMask;

    [SerializeField]
    private UnityEvent onDeflectEvent;

    [Header("On Impact")]
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float castSize = 1f;

    [SerializeField]
    private UnityEvent onImpactEvent;

    [SerializeField]
    private Collider ownCollider;

    private bool exploded = false;

    private void Awake()
    {
    }

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        if (!exploded)
        {
            MoveProjectile();
        }
    }

    private void FixedUpdate()
    {
        if (Time.time - spawnTime > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals(deflectTag))
        {
            DeflectProjectile(collision.collider);
        }

        else if (!collision.collider.Equals(ownCollider))
        {
            OnImpact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(deflectTag))
        {
            DeflectProjectile(other);
        }

        else if (!other.Equals(ownCollider))
        {
            OnImpact();
        }
    }

    public void OnImpact()
    {
        if (exploded)
        {
            return;
        }

        exploded = true;
        onImpactEvent.Invoke();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, castSize, transform.forward, castSize, layerMask);
        foreach (RaycastHit raycastHit in hits)
        {
            SwitchInteractable switchInteractable = raycastHit.collider.GetComponentInParent<SwitchInteractable>();
            if (switchInteractable)
            {
                switchInteractable.OnUse();
            }
            else
            {
                DestructibleObject destructibleObject = raycastHit.collider.GetComponentInParent<DestructibleObject>();
                if (destructibleObject)
                {
                    destructibleObject.Destroy();
                }

                else
                {
                    CharacterControllerScript character =
                        raycastHit.collider.GetComponentInParent<CharacterControllerScript>();
                    if (character&&!character.IsShielded)
                    {
                        character.KillCharacter();
                    }
                }
            }
        }
    }

    void MoveProjectile()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    public void Launch(Vector3 dir)
    {
        transform.rotation = Quaternion.identity;
        transform.forward = dir;
        direction = dir.normalized;
    }

    public void DeflectProjectile(Collider collider)
    {
        Vector3 dir = (collider.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, dir, out var raycast, castSize * 2, deflectLayerMask))
        {
            onDeflectEvent.Invoke();
            Launch(Vector3.Reflect(direction, raycast.normal));
        }
        else
        {
            Debug.LogError("deflection failed error");
        }
    }
}