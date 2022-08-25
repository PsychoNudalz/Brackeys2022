using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] cameraAngles;
    private int currentCamPosition;

    public void turnCamClockwise()
    {
        if (currentCamPosition == 3)
        {
            currentCamPosition = 0;
            cameraAngles[currentCamPosition].SetActive(true);
            cameraAngles[3].SetActive(false);
        }
        else
        {
            currentCamPosition++;
            cameraAngles[currentCamPosition].SetActive(true);
            cameraAngles[currentCamPosition - 1].SetActive(false);
        }  
    }

    public void turnCamAntiClockwise()
    {
        if (currentCamPosition == 0)
        {
            currentCamPosition = 3;
            cameraAngles[currentCamPosition].SetActive(true);
            cameraAngles[0].SetActive(false);
        }
        else
        {
            currentCamPosition--;
            cameraAngles[currentCamPosition].SetActive(true);
            cameraAngles[currentCamPosition + 1].SetActive(false);
        }
    }
}
