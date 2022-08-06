using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AirplaneLook : MonoBehaviourPun
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;
    float xRotation = 0f;

    public bool isInventoryOpened;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!isInventoryOpened)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
