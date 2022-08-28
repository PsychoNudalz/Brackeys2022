using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookAt : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform lookAtTarget;
    [SerializeField] private float sSpeed;

    void LateUpdate()
    {
        Vector3 lookDirection = lookAtTarget.position - mainCamera.position;
        lookDirection.Normalize();

        mainCamera.rotation = Quaternion.Slerp(mainCamera.rotation, Quaternion.LookRotation(lookDirection), sSpeed * Time.deltaTime);
    }
}
