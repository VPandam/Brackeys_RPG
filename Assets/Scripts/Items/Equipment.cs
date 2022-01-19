using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public int damageMultiplier, armorMultiplier;

    public EquipmentSlot equipmentSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredRegions;



    public override void Use()
    {
        base.Use();

        EquipmentManager.equipmentManagerInstance.EquipItem(this);
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Feet, Weapon, Shield };

//Corresponds to body blendshapes made for reduce the body size.
public enum EquipmentMeshRegion { Legs, Arms, Torso };


