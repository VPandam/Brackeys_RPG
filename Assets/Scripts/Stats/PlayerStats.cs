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

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
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
            Debug.Log(oldItem.damageMultiplier + " PlayerStats");
            damage.RemoveModifier(oldItem.damageMultiplier);
            armor.RemoveModifier(oldItem.armorMultiplier);
        }
    }
}
