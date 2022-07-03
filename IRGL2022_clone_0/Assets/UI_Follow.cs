using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Follow : MonoBehaviour
{
    public Camera maincamera;
    // Update is called once per frame
    void Update()
    {
        if (maincamera != null)
            transform.rotation = Quaternion.LookRotation(transform.position - maincamera.transform.position);
    }
}
