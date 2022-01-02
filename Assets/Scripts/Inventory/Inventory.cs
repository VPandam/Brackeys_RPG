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
    public bool AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (inventoryList.Count >= spaces)
            {
                Debug.Log("Not enough room in invetory");
            }
            else
            {
                inventoryList.Add(item);

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
