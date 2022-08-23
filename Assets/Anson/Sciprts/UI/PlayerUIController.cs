using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private PlayerUIInteractOptionController playerUIInteractOptionController;

    public static PlayerUIController current;

    public PlayerUIInteractOptionController PlayerUIInteractOptionController => playerUIInteractOptionController;

    private void Awake()
    {
        if (!current)
        {
            current = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        if (!playerUIInteractOptionController)
        {
            playerUIInteractOptionController = GetComponentInChildren<PlayerUIInteractOptionController>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
