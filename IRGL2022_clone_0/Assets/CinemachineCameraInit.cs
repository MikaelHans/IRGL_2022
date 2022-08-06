using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCameraInit : MonoBehaviour
{
    public GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<Camera>().enabled = false;
        mainCamera.GetComponent<Camera>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
