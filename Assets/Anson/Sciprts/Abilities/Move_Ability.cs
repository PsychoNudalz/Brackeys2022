using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Ability : Ability
{
    [Header("Move")]
    [SerializeField]
    private Collider characterCollider;

    [Header("Line of Sight")]
    [Space(10)]
    [Header("Select")]
    [SerializeField]
    private float select_range = 3f;

    [SerializeField]
    private LayerMask select_layerMask;

    [SerializeField]
    private LayerMask select_los_layerMask;


    [Space(10)]
    [Header("Move")]
    [SerializeField]
    private float move_range = 10f;

    [SerializeField]
    private LayerMask move_layerMask;

    [SerializeField]
    private LayerMask move_los_layerMask;

    [Header("Move Points")]
    [SerializeField]
    private List<MovePoint> movePoints;


    private void FixedUpdate()
    {
        CheckMovePointsInRange();
    }

    public override void OnUse(object target = null)
    {
        base.OnUse(target);
    }

    public override void OnUse_Enter(object target = null)
    {
        base.OnUse_Enter(target);
    }

    public override void OnUse_End(object target = null)
    {
        base.OnUse_End(target);
    }

    public override bool CanUse(object target = null)
    {
        if (target is DestructibleAbilityInteraction destructibleAbilityInteraction)
        {
        }
        else if (target is MovePointAbilityInteraction movePointAbilityInteraction)
        {
        }

        return false;
        
    }


    bool CheckLOS_Select(Vector3 target)
    {
        return CheckLOS(target, select_range, out var raycastHit, select_los_layerMask);
    }

    bool CheckLOS_Move(Vector3 target)
    {
        return CheckLOS(target, move_range, out var raycastHit, move_los_layerMask);
    }


    bool CheckLOS(Vector3 target, float range, out RaycastHit hit, LayerMask layerMask)
    {
        Vector3 dir = (transform.position - target).normalized;
        if (Physics.Raycast(target,
                dir,
                out hit, range, layerMask))
        {
            if (hit.collider.Equals(characterCollider))
            {
                Debug.DrawRay(target, dir * range, Color.green, 5f);
                return true;
            }
        }

        Debug.DrawRay(target, dir * range, Color.red, 5f);
        return false;
    }

    void CheckMovePointsInRange()
    {
        List<MovePoint> tempList = new List<MovePoint>();
        foreach (RaycastHit raycastHit in Physics.SphereCastAll(transform.position, move_range, transform.up,
                     move_range, move_layerMask))
        {
            if (raycastHit.collider.gameObject.TryGetComponent(out MovePoint movePoint) &&
                CheckLOS_Move(movePoint.Destination.position))
            {
                tempList.Add(movePoint);
                if (!movePoint.IsInRange)
                {
                    movePoint.OnInRange_Enter();
                }
            }
        }

        foreach (MovePoint movePoint in movePoints)
        {
            if (!tempList.Contains(movePoint))
            {
                movePoint.OnInRange_Exit();
            }
        }

        movePoints = tempList;
    }
}