using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovePoint : MonoBehaviour
{

    [SerializeField]
    private Transform destination;

    public Transform Destination => destination;

    [SerializeField]
    private UnityEvent inRange_Enter;
    [SerializeField]
    private UnityEvent inRange_Exit;

    [SerializeField]
    private bool isInRange = false;

    public bool IsInRange => isInRange;

    public void OnInRange_Enter()
    {
        inRange_Enter.Invoke();
        isInRange = true;
    }

    public void OnInRange_Exit()
    {
        inRange_Exit.Invoke();
        isInRange = false;
    }
    
}

