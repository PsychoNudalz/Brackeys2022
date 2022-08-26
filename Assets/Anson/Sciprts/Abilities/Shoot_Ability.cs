using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shoot_Ability : Ability
{


    [SerializeField]
    private SwitchInteractable currentSwitch;

    enum ArrowStateEnum
    {
        Idle,
        Firing,
        Stuck
    }

    [Header("Shoot")]
    [SerializeField]
    private Vector3 destination = new Vector3();

    [SerializeField]
    private GameObject arrowGO;

    [SerializeField]
    private LineRenderer arrowLine;

    [Header("Arrow Animation")]
    [SerializeField]
    private ArrowStateEnum arrowState = ArrowStateEnum.Idle;

    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float stopDistance;

    private Vector3 fireDir = new Vector3();


    [Header("Line of Sight")]
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Vector3 losOffset;
    [SerializeField]
    private float range = 100f;

    [SerializeField]
    private float arrowDetectionDelay = .2f;

    float arrowDetectionDelay_now;


    public SwitchInteractable CurrentSwitch => currentSwitch;

    private void Start()
    {
        ResetArrow();
        arrowGO.transform.parent = null;
    }
    
    
    private void Update()
    {
        if (arrowState == ArrowStateEnum.Firing)
        {
            FireArrowAnimation();

        }

        if (abilityStateEnum == AbilityStateEnum.Using)
        {
            UpdateLineToArrow();
        }
    }

    private void FixedUpdate()
    {
        if (arrowState is ArrowStateEnum.Stuck or ArrowStateEnum.Firing && abilityStateEnum == AbilityStateEnum.Using)
        {
            if (arrowState == ArrowStateEnum.Firing&&arrowDetectionDelay_now>=0)
            {
                arrowDetectionDelay_now -= Time.deltaTime;
            }
            if (!CheckLOS())
            {
                OnUse_End();
            }
        }
    }

    public override void OnUse(object target = null)
    {
        OnUse_Enter(target);
    }


    public override void OnUse_Enter(object target = null)
    {
        if (target is SwitchInteractable si)
        {
            if (currentSwitch)
            {
                SwitchInteractable temp = currentSwitch;
                OnUse_End(target);
                if (si.Equals(temp))
                {
                    return;
                }
            }
            
            ResetArrow();
            base.OnUse_Enter(target);
            currentSwitch = si;
            destination = si.transform.position;
            fireDir = (destination - transform.position).normalized;
            arrowGO.transform.position = transform.position;
            arrowGO.SetActive(true);
            arrowState = ArrowStateEnum.Firing;
            arrowDetectionDelay_now = arrowDetectionDelay;
        }
    }


    public override void OnUse_End(object target = null)
    {

        if (currentSwitch)
        {
            if (currentSwitch is ButtonSwitch buttonSwitch)
            {
                currentSwitch.LockCurrent(false);
            }
        }

        arrowState = ArrowStateEnum.Idle;
        ResetArrow();
        currentSwitch = null;
        
        base.OnUse_End(target);
    }

    private void ResetArrow()
    {
        arrowGO.SetActive(false);
        arrowGO.transform.position = transform.position;
        UpdateLineToArrow();
    }

    public override bool CanUse(object target = null)
    {
        return true;
    }

    bool CheckLOS()
    {
        if (destination.magnitude == 0)
        {
            return false;
        }

        if (arrowDetectionDelay_now > 0f)
        {
            return true;
        }

        Vector3 dir = (arrowGO.transform.position - transform.position+losOffset).normalized;
        if (Physics.Raycast(transform.position+losOffset,
                dir,
                out var raycastHit, range, layerMask))
        {
            if (raycastHit.collider.gameObject.Equals(arrowGO))
            {
                Debug.DrawRay(transform.position+losOffset, dir * range, Color.green, Time.deltaTime*2f);
                return true;
            }
        }

        Debug.DrawRay(transform.position+losOffset, dir * range, Color.red, Time.deltaTime*2f);
        return false;
    }

    void FireArrowAnimation()
    {
        if (destination.magnitude == 0)
        {
            return;
        }
        if (arrowState == ArrowStateEnum.Firing)
        {
            //Check if Arrow is in range of destination
            if (Vector3.Distance(destination, arrowGO.transform.position) <= stopDistance)
            {
                arrowState = ArrowStateEnum.Stuck;
                if (currentSwitch is ButtonSwitch buttonSwitch)
                {
                    if (buttonSwitch.InteractState_Current == InteractableObject.InteractState.Off)
                    {
                        buttonSwitch.OnUse();
                    }
                    currentSwitch.LockCurrent(true);
                }else if (currentSwitch is LeverSwitch leverSwitch)
                {
                    leverSwitch.OnUse();
                    OnUse_End(currentSwitch);
                }
            }
            else
            {
                arrowGO.transform.forward = fireDir;
                arrowGO.transform.position += fireDir * moveSpeed * Time.deltaTime;
            }
        }
    }

    void UpdateLineToArrow()
    {
        arrowLine.SetPositions(new Vector3[]{transform.position,arrowGO.transform.position});
    }
}