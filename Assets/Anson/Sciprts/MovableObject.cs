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

    public void PlayAnimator_Select()
    {
        animator.Play("Select");
    }

    public void PlayAnimator_Move_Enter()
    {
        animator.Play("Enter");
    }

    public void PlayAnimator_Move_End()
    {
        animator.Play("End");
    }

    IEnumerator MoveCoroutine()
    {
        moveState = MoveStateEnum.Move;
        PlayAnimator_Move_Enter();
        yield return new WaitForSeconds(moveDuration);
        PlayAnimator_Move_End();
        moveState = MoveStateEnum.None;

    }
}