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
    [SerializeField]
    public string ItemName;
    [SerializeField]
    public int level;

    public ItemData()
    {
        amount = 0;
        level = -1;
    }

    public void Use()
    {

    }

    public string Name { get => ItemName; set => ItemName = value; }
}
