using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item currentItem;
    public Image itemImage;

    public Button removeButton;

    public Text currentQuantityText;
    public int currentQuantity;


    public void DrawItem(Item item)
    {
        currentItem = item;
        itemImage.sprite = item.icon;
        itemImage.enabled = true;
        removeButton.interactable = true;
        if (item.currentQuantity > 1)
        {
            currentQuantity = item.currentQuantity;
            currentQuantityText.enabled = true;
            currentQuantityText.text = currentQuantity.ToString();
        }
        else
        {
            currentQuantityText.enabled = false;
        }

    }

    public void ClearSlot()
    {
        currentItem = null;
        itemImage.sprite = null;
        itemImage.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.inventoryInstance.RemoveItem(currentItem);
    }

    public void UseItem()
    {
        if (currentItem != null)
        {
            currentItem.Use();
        }
    }

}
