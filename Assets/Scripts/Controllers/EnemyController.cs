using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    bool lookTarget;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, lookRadius);

    }

    private void Start()
    {
        target = PlayerManager.sharedInstance.player.transform;
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, this.transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                //Attack target
                lookTarget = true;
                StartCoroutine("FollowTarget");
            }

        }
    }


    IEnumerator FollowTarget()
    {
        while (lookTarget)
        {

            FaceTarget();
            yield return new WaitForSeconds(0.2f);

        }
    }

    void FaceTarget()
    {
        Vector3 lookDirection = (target.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0f, lookDirection.z));
        transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
