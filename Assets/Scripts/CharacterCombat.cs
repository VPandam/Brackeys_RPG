using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats stats;

    //Time between attacks
    float attackCooldown = 1.5f;

    float timeToWait;
    float counter = 0;
    public event System.Action<CharacterStats, CharacterStats> OnAttack;
    public bool InCombat;
    public float combatCooldown = 5;
    float lastAttackTime;
    private void Start()
    {
        stats = this.GetComponent<CharacterStats>();
    }

    private void Update()
    {
        counter -= Time.deltaTime;
        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }

    }
    public void Attack(CharacterStats targetStats)
    {

        if (counter <= 0)
        {
            if (OnAttack != null)
                OnAttack(stats, targetStats);

            InCombat = true;
            


            float percentage = (1 + ((float)stats.attackSpeedPerc.GetValue() / 100));
            timeToWait = Mathf.Clamp(attackCooldown / percentage, 0.5f, 2);
            counter = timeToWait;

            lastAttackTime = Time.time;

        }

    }
}
