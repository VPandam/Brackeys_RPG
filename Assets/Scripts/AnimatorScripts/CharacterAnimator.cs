using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    const float locomationAnimationSmoothTime = .1f;

    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;
    public AnimatorOverrideController overrideController;
    int attackIndex;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        //Allows to change some clip animations on execution time.
        if (overrideController == null)
        {

            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;

        //If we dont override this method
        //Sets the default attack animation set as the current one
        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;

    }

    protected virtual void Update()
    {

        float speedPercent = agent.velocity.magnitude / agent.speed;

        //Movement
        if (animator != null)
        {

            animator.SetFloat("speedPerc", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);

            animator.SetBool("inCombat", combat.InCombat);
        }
    }

    //When an attack is made, trigger the current attack animation and call the take damage coroutine
    protected virtual void OnAttack(CharacterStats stats, CharacterStats targetStats)
    {

        if (animator != null)
        {

            animator.SetTrigger("attack");
            attackIndex = Random.Range(0, currentAttackAnimSet.Length);
            overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
        }
        StartCoroutine(TakeDamage(stats, targetStats));

    }

    //Waits until half of the duration of the actual attack clip animation 
    //so the HP and the UI is updated more or less when the attack touches the enemy.
    IEnumerator TakeDamage(CharacterStats stats, CharacterStats targetStats)
    {
        yield return new WaitForSeconds(((currentAttackAnimSet[attackIndex].length) - ((currentAttackAnimSet[attackIndex].length) / 2)));
        targetStats.TakeDamage(stats.damage.GetValue());
        if (targetStats.currentHealth <= 0 || targetStats == null)
        {
            stats.gameObject.GetComponent<CharacterCombat>().InCombat = false;
        }
    }
}
