using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    public Transform itemParent;

    public List<InventorySlotUI> slots;

    public Player player;

    public ChracterPickUpWeapon weapon_controller;

    public InventorySlotUI helmet, armor;

    public List<InventorySlotUI> weapon;

    //public InventorySlotUI[] slots;

    // Start is called before the first frame update
    void Awake()
    {
        slots.AddRange(itemParent.GetComponentsInChildren<InventorySlotUI>(true));
        for (int i = 0; i < inventory.spaceLimit; i++)
        {
            slots[i].inventory = inventory;
            slots[i].gameObject.GetComponent<Image>().enabled = true;
        }
    }

    public void InitUI()
    {
        for (int i = 0; i < inventory.spaceLimit; i++)  
        {
            slots[i].gameObject.GetComponent<Image>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        for(int i = 0; i < slots.Count; i++)
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

        for(int i = 0; i < weapon.Count; i++)
        {
            //if (weapon_controller.weapon[i]._gunsystem)
            
            if (weapon_controller.weapon[i]._gunsystem != null)
            {
                weapon[i].AddItem(weapon_controller.weapon[i]);
            }            
        }

        if(player.Helmet.prefab != null)
        {
            helmet.AddItem(player.Helmet);
        }
        if(player.Armor.prefab != null)
        {
            armor.AddItem(player.Armor);
        }       
    }

    public void removeAll()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if(slots[i].item != null)
            {
                slots[i].removeItem();
            }
        }
    }
}
