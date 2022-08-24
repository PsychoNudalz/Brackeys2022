using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public enum MoveStateEnum
    {
        None,
        Selected,
        Move
    }

    [SerializeField]
    private MoveStateEnum moveState;

    [SerializeField]
    private MovePoint currentMovePoint;

    [SerializeField]
    private Collider[] colliders;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float moveDuration = 1f;

    private void Awake()
    {
        if (colliders.Length == 0)
        {
            colliders = GetComponentsInChildren<Collider>();
        }

        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public void OnSelect()
    {
        moveState = MoveStateEnum.Selected;
        PlayAnimator_Select();
    }

    public void OnMove(MovePoint movePoint)
    {
        currentMovePoint = movePoint;
        StartCoroutine(MoveCoroutine());
    }

    void SetCollidersActive(bool b)
    {
        foreach (Collider c in colliders)
        {
            c.enabled = b;
        }
    }

    public void PlayAnimator_Select()
    {
        animator.Play("Select");
        SetCollidersActive(false);
        
    }

    public void PlayAnimator_Move_Enter()
    {
        animator.Play("Enter");
    }

    public void PlayAnimator_Move_End()
    {
        animator.Play("End");
        SetCollidersActive(true);

    }

    IEnumerator MoveCoroutine()
    {
        moveState = MoveStateEnum.Move;
        PlayAnimator_Move_Enter();
        yield return new WaitForSeconds(moveDuration);
        transform.position = currentMovePoint.Destination.position;
        transform.rotation = currentMovePoint.Destination.rotation;
        PlayAnimator_Move_End();
        moveState = MoveStateEnum.None;

    }
}