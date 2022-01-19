using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public Stat damage;
    public Stat armor;
    public Stat attackSpeedPerc;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        int damageDealt = Mathf.Clamp((damageAmount - armor.GetValue()), 0, int.MaxValue);

        currentHealth -= damageDealt;
        Debug.Log(gameObject.name + " takes " + damageDealt + " damage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " died");
    }
}
