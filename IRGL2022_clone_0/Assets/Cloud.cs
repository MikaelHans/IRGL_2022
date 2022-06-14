using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Item> cloud = new List<Item>();
    void Start()
    {
        //for editor only
        cloud.AddRange(Resources.LoadAll<Item>("Prefabs"));
        DontDestroyOnLoad(gameObject);
    }
}
