using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public int amount;
    public string name;

    public ItemData()
    {
        amount = 0;
    }
}
