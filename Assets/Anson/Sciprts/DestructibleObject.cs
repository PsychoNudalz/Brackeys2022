using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructibleObject : MonoBehaviour
{

    [SerializeField]
    private UnityEvent onDestroyEvent;

    public void Destroy()
    {
        onDestroyEvent.Invoke();
    }
}
