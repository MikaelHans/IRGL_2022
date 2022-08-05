using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetLayerRecursively(gameObject, LayerMask.NameToLayer("MyMinimap"));
    }

    public static void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
