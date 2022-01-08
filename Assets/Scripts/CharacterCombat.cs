using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats stats;

    //Time between attacks
    float attackCooldown = 1.5f;
    float counter = 0;

    private void Start()
    {
        stats = this.GetComponent<CharacterStats>();
    }

    private void Update()
    {
        counter -= Time.deltaTime;
    }
    public void Attack(CharacterStats targetStats)
    {

        if (counter <= 0)
        {
            targetStats.TakeDamage(stats.damage.GetValue());
            counter = attackCooldown / (float)stats.attackSpeed.GetValue();
        }

    }
}
