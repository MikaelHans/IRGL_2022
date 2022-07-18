using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainCamera;
    public GameObject mapCamera;

    public bool isMapOpened = false;
    public bool isMapEnabledScene = false;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        mapCamera = GameObject.Find("Map Camera");

        if (mapCamera == null)
        {
            isMapEnabledScene = false;

        }
        else
        {
            isMapEnabledScene = true;
        }

        mainCamera.GetComponent<Camera>().enabled = false;
        mainCamera.GetComponent<Camera>().enabled = true;
    }

    public void Map()
    {
        if (!isMapEnabledScene)
        {
            return;
        }
        if (!isMapOpened)
        {
            isMapOpened = true;
            OpenMap();
        }
        else
        {
            isMapOpened = false;
            CloseMap();
        }
    }

    public void OpenMap()
    {
        mapCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 100;
    }

    public void CloseMap()
    {
        mapCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
