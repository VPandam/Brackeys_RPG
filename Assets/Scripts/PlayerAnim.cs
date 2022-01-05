using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnim : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;

    float locomotionAnimationSmoothTime = 0.1f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float speedPerc = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("SpeedPerc", speedPerc, locomotionAnimationSmoothTime, Time.deltaTime);
    }
}
