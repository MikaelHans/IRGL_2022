using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    public Transform itemParent;

    public InventorySlotUI[] slots;
    // Start is called before the first frame update
    void Awake()
    {
        slots = itemParent.GetComponentsInChildren<InventorySlotUI>();
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].inventory = inventory;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void removeAll()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                slots[i].removeItem();
            }
        }
    }
}
