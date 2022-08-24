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

    private float lastShotTime = 0;

    [SerializeField]
    private UnityEvent onShootEvent;

    private void Update()
    {
        if (Time.time - lastShotTime > shootTime)
        {
            OnShoot();
        }
    }

    public void OnShoot()
    {
        lastShotTime = Time.time;
        projectile = Instantiate(projectileGO,launchPoint.position,launchPoint.rotation).GetComponent<Projectile>();
        projectile.Launch(launchPoint.forward);
        onShootEvent.Invoke();
    }

    private void OnEnable()
    {
        OnShoot();
    }
}
