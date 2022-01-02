using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreWorldBorder : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject worldborder;
    void Start()
    {
        Physics.IgnoreCollision(worldborder.GetComponentInChildren<CapsuleCollider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(worldborder.GetComponent<MeshCollider>(), GetComponent<Collider>());
    }
}
