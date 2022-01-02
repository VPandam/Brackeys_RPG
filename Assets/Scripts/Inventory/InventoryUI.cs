using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public GameObject itemsParent;

    public GameObject inventoryUI;

    InventorySlot[] inventorySlots;

    private void Awake()
    {
        if (inventoryUI == null)
            inventoryUI = this.gameObject;

    }
    void Start()
    {
        inventory = Inventory.inventoryInstance;
        //OnChangeInvenctoryCallback is called every time we add or remove an item.
        inventory.OnChangeInventoryCallback += UpdateInventoryUI;

        inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Inventory"))
            inventoryUI.SetActive(!inventoryUI.activeSelf);

    }
    void UpdateInventoryUI()
    {

        //Iterates over all the slots in our inventory.
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            //If there is another item to add, draw it in the inventoryUI
            //else clear it.
            if (i < inventory.inventoryList.Count)
            {
                inventorySlots[i].DrawItem(inventory.inventoryList[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }

        }

    }
}
