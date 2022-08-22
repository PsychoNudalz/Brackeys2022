using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractOptionController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private bool isActive;

    public bool IsActive => isActive;

    private void Awake()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }

        SetWheelActive(false);
    }

    public void SetWheelActive(bool b, Vector3 pos = new Vector3())
    {
        if (b)
        {
            animator.SetBool("OpenWheel", true);
        }
        else
        {
            animator.SetBool("OpenWheel", false);
        }

        isActive = b;
        if (pos.magnitude > .001f)
        {
            transform.position = Camera.main.WorldToScreenPoint(pos);
        }
    }

    /// <summary>
    /// Animation only
    /// </summary>
    public void DisableWheel()
    {
        gameObject.SetActive(false);
    }

    public void ActivateWheel(Vector3 pos)
    {
        gameObject.SetActive(true);
        SetWheelActive(true, pos);
    }
}