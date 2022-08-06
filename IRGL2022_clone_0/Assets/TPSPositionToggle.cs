using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TPSPositionToggle : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isLeftPositioned = false;
    public CinemachineVirtualCamera tpsCamera;
    public Vector3 defaultOffset;

    void Start()
    {
        defaultOffset = tpsCamera.GetComponentInChildren<Cinemachine3rdPersonFollow>().ShoulderOffset;
    }

    public void TogglePosition()
    {
        isLeftPositioned = !isLeftPositioned;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentOffset = tpsCamera.GetComponentInChildren<Cinemachine3rdPersonFollow>().ShoulderOffset;
        if (isLeftPositioned)
        {
            tpsCamera.GetComponentInChildren<Cinemachine3rdPersonFollow>().ShoulderOffset += ((defaultOffset * -1) - currentOffset) * 0.1f;
        }
        else
        {
            tpsCamera.GetComponentInChildren<Cinemachine3rdPersonFollow>().ShoulderOffset += (defaultOffset - currentOffset) * 0.1f;
        }
    }
}
