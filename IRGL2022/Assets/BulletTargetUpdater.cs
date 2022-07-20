using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BulletTargetUpdater : MonoBehaviour
{
    public GameObject bulletTargetRender;
    public GameObject fpsCamera;
    public RaycastHit rayHit;
    public GameObject characterModel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bulletTargetRender.transform.position = fpsCamera.transform.position + fpsCamera.transform.forward * 100;
        if (Physics.Raycast(fpsCamera.transform.position + fpsCamera.transform.forward, fpsCamera.transform.forward, out rayHit))
        {
            if (rayHit.collider)
            {
                bulletTargetRender.transform.position = rayHit.point;
            }
        }
        characterModel.transform.LookAt(bulletTargetRender.transform);
    }
}
