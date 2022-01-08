using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager equipmentManagerInstance;

    Equipment[] currentEquipment;
    public Equipment[] defaultEquipment;
    SkinnedMeshRenderer[] currentMeshes;
    public SkinnedMeshRenderer targetMesh;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    Inventory inventory;



    private void Start()
    {
        //Get the number of elements in an enum
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        //Initialize our array with one space for each slot.
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        inventory = Inventory.inventoryInstance;
        targetMesh = targetMesh.GetComponent<SkinnedMeshRenderer>();

        EquipDefaultItems();

    }
    private void Awake()
    {
        if (equipmentManagerInstance == null)
        {
            equipmentManagerInstance = this;
        }
    }

    /// <summary>
    /// Equips an equipment of our inventory to the currentEquipment array.
    /// </summary>
    /// <param name="newEquipment"></param>
    public void EquipItem(Equipment newEquipment)
    {


        //Gets the index of the new equipment in the equipmentSlot enum.
        //This will be the array position for each slot.
        int equipmentSlot = (int)newEquipment.equipmentSlot;


        Equipment oldEquipment = Unequip(equipmentSlot);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newEquipment, oldEquipment);
        }
        //Removes the new equipment from the inventory and adds it to the current equipment array
        newEquipment.RemoveFromInventory();
        currentEquipment[equipmentSlot] = newEquipment;

        SetEquipmentBlendShapes(newEquipment, 100);

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newEquipment.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[equipmentSlot] = newMesh;
    }

    /// <summary>
    /// If there's an item in the specified slot, unequip it returning it to the inventory.
    /// </summary>
    /// <param name="slotIndex"></param>
    public Equipment Unequip(int slotIndex)
    {
        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);
            currentEquipment[slotIndex] = null;
            SetEquipmentBlendShapes(oldItem, 0);

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
        return oldItem;

    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    public void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendshape in item.coveredRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendshape, weight);
        }
    }

    public void EquipDefaultItems()
    {
        foreach (Equipment equipment in defaultEquipment)
        {
            EquipItem(equipment);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }






}
