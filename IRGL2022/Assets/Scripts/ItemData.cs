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
    private string ItemName;

    public ItemData()
    {
        amount = 0;
    }

    public string Name { get => ItemName; set => ItemName = value; }
}
