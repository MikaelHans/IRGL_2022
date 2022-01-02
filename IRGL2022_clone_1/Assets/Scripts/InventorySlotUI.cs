using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI stack;
    public Image icon;
    public Inventory inventory;


    public Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
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

    public void onUseItem()
    {
        inventory.UseItemInInventory(item);
    }
}
