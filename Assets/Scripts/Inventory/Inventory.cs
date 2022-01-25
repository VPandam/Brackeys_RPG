using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventoryInstance;
    public List<Item> inventoryList = new List<Item>();
    public int spaces = 20;

    public delegate void OnChangeInventory();
    public OnChangeInventory OnChangeInventoryCallback;
    bool isInList;

    private void Awake()
    {
        if (inventoryInstance == null)
        {
            inventoryInstance = this;
        }
        else
        {
            Debug.Log("More than one inventory instance");
        }
    }

    /// <summary>
    /// Add an item to the inventory list.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>True if the item is added, false if theres not enough space.</returns>
    public bool AddItem(Item item, int quantity)
    {
        Item copyItem = item.GetCopy();
        isInList = false;

        if (!item.isDefaultItem)
        {
            foreach (Item i in inventoryList)
            {
                if (i.name == item.name && i.currentQuantity < 99)
                {
                    isInList = true;
                    i.currentQuantity += quantity;

                    if (i.currentQuantity > i.maxStackSize)
                    {
                        int surplus = i.currentQuantity - i.maxStackSize;
                        i.currentQuantity -= surplus;
                        if (OnChangeInventoryCallback != null)
                        {
                            OnChangeInventoryCallback.Invoke();
                        }
                        AddItem(item, surplus);
                    }
                    else
                    {

                        if (OnChangeInventoryCallback != null)
                        {
                            OnChangeInventoryCallback.Invoke();
                        }
                    }
                    return true;

                }

            }
            if (!isInList)
            {
                if (inventoryList.Count >= spaces)
                {
                    Debug.Log("Not enough room in invetory");
                }
                else
                {
                    copyItem.currentQuantity += quantity;
                    inventoryList.Add(copyItem);
                }
                if (OnChangeInventoryCallback != null)
                {
                    OnChangeInventoryCallback.Invoke();
                }
                return true;
            }
        }

        return false;
    }



    /// <summary>
    /// Removes an item of the inventory list.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>True if the item is removed.</returns>
    public bool RemoveItem(Item item)
    {

        inventoryList.Remove(item);

        if (OnChangeInventoryCallback != null)
        {
            OnChangeInventoryCallback.Invoke();
        }

        return true;
    }
}
