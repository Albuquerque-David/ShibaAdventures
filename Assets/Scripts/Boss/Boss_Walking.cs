using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss_Walking : StateMachineBehaviour
{
    private EnemyController bossEnemyController;
    BossIA boss;
    Transform player;
    Rigidbody2D rb;
    [SerializeField]
    private float speed = 2f;
    public float attackRange = 3.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossEnemyController = animator.GetComponent<EnemyController>();
        boss = animator.GetComponent<BossIA>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.CheckDirection();

        float dist = rb.transform.position.x - player.transform.position.x;


        if (dist > 0)
        {
            if(dist >= 1)
            {

                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
        }

        else
        {
            if(dist <= -1)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
        }


        if(Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            if (bossEnemyController.getHealth() <= (bossEnemyController.getMaxHealth() - 30))
                animator.SetTrigger("Attack2");
            else
                animator.SetTrigger("Attack");
        }

        if(bossEnemyController.getHealth() <= (bossEnemyController.getMaxHealth() / 2))
        {
            animator.SetBool("IsEnraged", true);
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack2");
    }
}
