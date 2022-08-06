using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI stack;
    public Image icon;
    public Inventory inventory;


    public ItemData item;
    Drop drop;

    private void Start()
    {
        drop = GetComponentInParent<Player>().drop;
    }

    public void AddItem(ItemData newItem)
    {
        item = newItem;

        icon.sprite = item.prefab.GetComponent<Item>().icon;
        icon.enabled = true;

        stack.text = item.amount.ToString();
        stack.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        stack.text = "";
        stack.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            int itemDrop = Mathf.Max(item.prefab.GetComponent<Item>().maxStack, item.amount);
            item.amount -= itemDrop;
            inventory.Remove(item);            
            drop.DropItem(item,itemDrop);
            //Debug.Log(eventData.);
        }
    }
    
    public void removeItem()
    {
        inventory.Remove(item);
    }

    public void onUseItem()
    {
        if (item.prefab.GetComponent<IUsable>() != null && item.prefab != null)
        {
            inventory.UseItemInInventory((UsableItemData)item);
        }
        else
        {
            Debug.Log("error, nullreference exception");
        }
    }
}
