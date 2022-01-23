using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.equipmentManagerInstance.onEquipmentChanged += OnEquipmentChanged;
    }


    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            damage.AddModifier(newItem.damageMultiplier);
            armor.AddModifier(newItem.armorMultiplier);
        }
        if (oldItem != null)
        {
            damage.RemoveModifier(oldItem.damageMultiplier);
            armor.RemoveModifier(oldItem.armorMultiplier);
        }
    }
    public override void Die()
    {
        base.Die();
        //Add ragdoll effect

        Destroy(this.gameObject);
    }
}
