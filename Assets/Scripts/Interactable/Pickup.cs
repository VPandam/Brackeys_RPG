
using UnityEngine;

public class Pickup : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        Debug.Log("Pickingup" + item.name);
        PickUpItem();
    }

    /// <summary>
    /// Pickup an item if there is enough space in inventary.
    /// </summary>
    void PickUpItem()
    {
        bool pickedUp = Inventory.inventoryInstance.AddItem(item);
        if (pickedUp)
        {
            Destroy(this.gameObject);
        }
    }
}
