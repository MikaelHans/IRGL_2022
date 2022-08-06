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

    public BulletTargetUpdater bulletTargetUpdater;

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
            bulletTargetUpdater.syncCrosshair = false;
        }
        else
        {
            fpsCamera.Priority = -1;
            tpsCamera.Priority = 3;
            bulletTargetUpdater.syncCrosshair = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
