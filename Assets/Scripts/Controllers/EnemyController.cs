using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    CharacterStats targetStats;
    CharacterStats enemyStats;
    NavMeshAgent agent;

    CharacterCombat enemyCombat;
    bool isOnAttackDistance;

    bool attacking;
    bool movingToFaceTarget;



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, lookRadius);

    }

    private void Start()
    {
        target = PlayerManager.sharedInstance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyCombat = GetComponent<CharacterCombat>();
        enemyStats = GetComponent<CharacterStats>();
        targetStats = target.gameObject.GetComponent<CharacterStats>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, this.transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            isOnAttackDistance = distance <= agent.stoppingDistance;
            if (isOnAttackDistance)
            {
                if (!attacking)
                {
                    StartCoroutine("AttackTarget");
                    attacking = true;
                }
                if (!movingToFaceTarget)
                {
                    StartCoroutine("FaceTargetCo");
                }
            }


        }
    }
    IEnumerator AttackTarget()
    {
        while (isOnAttackDistance)
        {

            enemyCombat.Attack(targetStats);
            yield return new WaitForEndOfFrame();

        }

        attacking = false;
    }


    IEnumerator FaceTargetCo()
    {
        while (isOnAttackDistance)
        {

            FaceTarget();
            yield return new WaitForSeconds(0.2f);

        }
        movingToFaceTarget = false;
    }

    void FaceTarget()
    {
        Vector3 lookDirection = (target.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0f, lookDirection.z));
        transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
