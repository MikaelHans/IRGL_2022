using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BulletTargetUpdater : MonoBehaviour
{
    public GameObject bulletTargetRender;
    public GameObject fpsCamera;
    public RaycastHit rayHit;
    // public GameObject characterModel;

    public GameObject crosshair;

    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletTargetRender.transform.position = fpsCamera.transform.position + fpsCamera.transform.forward * 100;
        if (Physics.Raycast(fpsCamera.transform.position + fpsCamera.transform.forward, fpsCamera.transform.forward, out rayHit, 1000))
        {
            if (rayHit.collider)
            {
                bulletTargetRender.transform.position = rayHit.point;
            }
        }

        crosshair.transform.position = mainCamera.WorldToScreenPoint(bulletTargetRender.transform.position);

        // characterModel.transform.LookAt(bulletTargetRender.transform);
    }
}
