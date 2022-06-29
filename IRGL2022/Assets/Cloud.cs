using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Item> cloud = new List<Item>();
    public string teamName;
    void Start()
    {
        //for editor only
        int _id = 0;
        cloud.AddRange(Resources.LoadAll<Item>("Prefabs"));
        foreach (Item item in cloud)
        {
            item.id = _id++;
        }
        DontDestroyOnLoad(gameObject);
    }
}
