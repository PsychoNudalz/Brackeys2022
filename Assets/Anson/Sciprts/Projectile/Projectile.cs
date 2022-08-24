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

    [Header("On Impact")]
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float castSize = 1f;

    [SerializeField]
    private UnityEvent onImpactEvent;

    private bool exploded = false;

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
        OnImpact();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnImpact();
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
                    CharacterControllerScript character = raycastHit.collider.GetComponentInParent<CharacterControllerScript>();
                    if (character)
                    {
                        character.killCharacter();
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
}