using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject projectileGO;

    [SerializeField]
    private Projectile projectile;

    [SerializeField]
    private Transform launchPoint;

    [SerializeField]
    private float shootTime = 1;

    [SerializeField]
    private float lastShotTime = 0;

    [SerializeField]
    private UnityEvent onShootEvent;

    [SerializeField]
    private UnityEvent onOnEvent;

    [SerializeField]
    private UnityEvent onOffEvent;

    private bool isActive = true;

    private void Update()
    {
        if (isActive && shootTime != 0)
        {
            if (Time.time - lastShotTime > shootTime)
            {
                OnShoot();
            }
        }
    }

    public void OnShoot()
    {
        lastShotTime = Time.time;
        projectile = Instantiate(projectileGO, launchPoint.position, launchPoint.rotation).GetComponent<Projectile>();
        projectile.Launch(launchPoint.forward);
        onShootEvent.Invoke();
    }

    // private void OnEnable()
    // {
    //     SetActive(true);
    // }

    public void SetActive(bool b)
    {
        if (b)
        {
            OnShoot();
            onOnEvent.Invoke();
        }
        else
        {
            onOffEvent.Invoke();
        }

        isActive = b;
    }
}