using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{

    NavMeshAgent navMeshAgent;

    Interactable followTarget;
    bool following = false;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget != null)
        {
            FaceTarget();
            if (!following)
            {
                StartCoroutine("FollowTarget");
                following = true;
            }

        }
    }

    public void MoveToPoint(Vector3 point)
    {
        navMeshAgent.SetDestination(point);
    }

    public void SetTargetToFollow(Interactable interactable)
    {
        following = false;
        navMeshAgent.stoppingDistance = interactable.radius * 0.8f;
        navMeshAgent.updateRotation = false;
        followTarget = interactable;
    }
    public void StopFollowTarget()
    {
        navMeshAgent.stoppingDistance = 0;
        navMeshAgent.updateRotation = true;
        followTarget = null;
        following = false;
    }

    IEnumerator FollowTarget()
    {
        while (followTarget != null)
        {
            navMeshAgent.SetDestination(followTarget.interactionTransform.position);

            yield return new WaitForSeconds(0.2f);

        }
    }

    void FaceTarget()
    {
        Vector3 lookDirection = (followTarget.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0f, lookDirection.z));
        transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
