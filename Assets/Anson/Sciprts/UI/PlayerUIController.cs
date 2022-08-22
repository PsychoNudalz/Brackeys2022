using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private PlayerInteractOptionController playerInteractOptionController;

    public static PlayerUIController current;

    public PlayerInteractOptionController PlayerInteractOptionController => playerInteractOptionController;

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
        
        if (!playerInteractOptionController)
        {
            playerInteractOptionController = GetComponentInChildren<PlayerInteractOptionController>();
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
