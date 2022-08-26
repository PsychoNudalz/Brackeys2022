using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovePoint : MonoBehaviour
{
    enum MovePointEnum
    {
        Empty,
        InRange,
        Occupied
    }

    private MovePointEnum movePointState = MovePointEnum.Empty;

    [SerializeField]
    private Transform destination;

    public Transform Destination => destination;

    [SerializeField]
    private UnityEvent inRange_Enter;

    [SerializeField]
    private UnityEvent inRange_Exit;


    [SerializeField]
    private MovableObject currentObject;

    public bool IsInRange => movePointState == MovePointEnum.InRange;

    public MovableObject CurrentObject => currentObject;

    public void OnInRange_Enter()
    {
        if (movePointState == MovePointEnum.Empty)
        {
            inRange_Enter.Invoke();
            movePointState = MovePointEnum.InRange;
        }
    }

    public void OnInRange_Exit()
    {
        if (movePointState is MovePointEnum.InRange or MovePointEnum.Occupied)
        {
            inRange_Exit.Invoke();
            if (!currentObject)
            {
                movePointState = MovePointEnum.Empty;
            }
            else
            {
                movePointState = MovePointEnum.Occupied;
            }
        }
    }

    public void OnMove_Enter(MovableObject m)
    {
        currentObject = m;
        OnInRange_Exit();
    }

    public void OnMove_Exit()
    {
        currentObject = null;
        OnInRange_Exit();

    }
}