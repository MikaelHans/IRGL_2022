using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class FPSTPSToggle : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFPSMode = false;
    public CinemachineVirtualCamera fpsCamera;
    public CinemachineVirtualCamera adsCamera;
    public CinemachineVirtualCamera tpsCamera;

    void Start()
    {

    }

    public void ToggleMode()
    {
        isFPSMode = !isFPSMode;
        if (isFPSMode)
        {
            fpsCamera.Priority = 1;
            adsCamera.Priority = -1;
            tpsCamera.Priority = -1;
        }
        else
        {
            fpsCamera.Priority = -1;
            tpsCamera.Priority = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
