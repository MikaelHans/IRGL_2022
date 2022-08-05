using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponHandlingMode : MonoBehaviour
{
    public bool isAiming = false;
    public GameObject gunContainer;
    public GameObject aimingParent;
    public GameObject idleParent;
    public RigBuilder rigBuilder;
    public Transform idleTransform;
    public Transform aimingTransform;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetAiming(bool aiming)
    {
        isAiming = aiming;

    }


    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            gunContainer.transform.SetParent(aimingParent.transform, false);
            gunContainer.transform.localPosition = aimingTransform.localPosition;
            gunContainer.transform.localRotation = aimingTransform.localRotation;

            foreach (var rig in rigBuilder.layers)
            {
                rig.rig.weight += (1f - rig.rig.weight) * Time.deltaTime * 10;
                // rig.rig.weight = 1;
            }
        }
        else
        {
            // gunContainer.transform.SetPositionAndRotation(originalPosition, originalRotation);
            gunContainer.transform.SetParent(idleParent.transform, false);
            gunContainer.transform.localPosition = idleTransform.localPosition;
            gunContainer.transform.localRotation = idleTransform.localRotation;
            foreach (var rig in rigBuilder.layers)
            {
                rig.rig.weight += (0f - rig.rig.weight) * Time.deltaTime * 10;
                // rig.rig.weight = 0;
            }
        }

        isAiming = false;
    }
}
