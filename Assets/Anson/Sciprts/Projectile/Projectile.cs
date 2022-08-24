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
    private List<string> tagList;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float castSize = 1f;

    [SerializeField]
    private UnityEvent onImpactEvent;

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        MoveProjectile();
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
        if (tagList.Contains(collision.collider.tag))
        {
            OnImpact();
        }
    }

    public void OnImpact()
    {
        onImpactEvent.Invoke();
        enabled = false;
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, castSize, transform.forward, castSize, layerMask);
        foreach (RaycastHit raycastHit in hits)
        {
            SwitchInteractable switchInteractable = GetComponentInParent<SwitchInteractable>();
            if (switchInteractable)
            {
                switchInteractable.OnUse();
            }
            else
            {
                CharacterControllerScript character = GetComponentInParent<CharacterControllerScript>();
                if (character)
                {
                    character.DealDamage(1f);
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