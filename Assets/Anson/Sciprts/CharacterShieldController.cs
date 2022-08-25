using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterShieldController : MonoBehaviour
{

    [SerializeField]
    private bool isActive;

    [SerializeField]
    private GameObject shieldGO;

    [SerializeField]
    private UnityEvent onActivate;

    [SerializeField]
    private UnityEvent onDeactivate;

    public bool IsActive => isActive;


    private void Start()
    {
        SetActive(isActive);
    }

    public void SetActive(bool b)
    {
        
        if (b)
        {
            onActivate.Invoke();
            shieldGO.SetActive(true);
        }
        else
        {
            onDeactivate.Invoke();
            shieldGO.SetActive(false);
        }

        isActive = b;
    }
}
