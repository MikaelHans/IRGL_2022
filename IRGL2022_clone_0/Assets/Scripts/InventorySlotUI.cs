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
            inventory.Remove(item);
        }
    }
    
    public void removeItem()
    {
        inventory.Remove(item);
    }

    public void onUseItem()
    {
        if (item is UsableItemData)
        {
            inventory.UseItemInInventory((UsableItemData)item);
        }        
    }
}
