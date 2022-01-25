
using UnityEngine;

public class Pickup : Interactable
{
    public Item item;

    [Range(0, 100)]
    public int quantity = 1;
    public override void Interact()
    {
        base.Interact();

        Debug.Log("Pickingup" + item.name);
        PickUpItem();
    }

    /// <summary>
    /// Pickup an item if there is enough space in inventary.
    /// </summary>
    protected virtual void PickUpItem()
    {
        bool pickedUp = Inventory.inventoryInstance.AddItem(item, quantity);
        if (pickedUp)
        {
            Destroy(this.gameObject);
        }
    }
}
