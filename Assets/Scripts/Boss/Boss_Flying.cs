using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Flying : StateMachineBehaviour
{
    private EnemyController bossEnemyController;
    BossIA boss;
    Transform player;
    Rigidbody2D rb;
    private float speed = 6f;
    public float attackRange = 3.0f;

    public Transform rightPosition;
    public Transform leftPosition;

    private bool movingRight = true;

    public int changeSide = 7;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossEnemyController = animator.GetComponent<EnemyController>();
        boss = animator.GetComponent<BossIA>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();


        Transform[] staticPositions = FindObjectsOfType<Transform>();

        foreach(Transform staticPosistion in staticPositions)
        {
            if (staticPosistion.CompareTag("BossRightPosition"))
                this.rightPosition = staticPosistion;
            if (staticPosistion.CompareTag("BossLeftPosition"))
                this.leftPosition = staticPosistion;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (movingRight)
        {
            if (!boss.GetDirection())
                boss.FlipToRight();
            Vector2 target = new Vector2(rightPosition.position.x, rightPosition.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        else
        {
            if (!boss.GetDirection())
                boss.FlipToLeft();
            Vector2 target = new Vector2(leftPosition.position.x, leftPosition.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }

        float distR = rb.transform.position.x - rightPosition.transform.position.x;
        float distL = rb.transform.position.x - leftPosition.transform.position.x;

        if(distR == 0)
        {
            boss.FlipToLeft();
            animator.SetTrigger("Attack3");
            changeSide--;
        }

        if(distL == 0)
        {
            boss.FlipToRight();
            animator.SetTrigger("Attack3");
            changeSide--;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(changeSide == 0)
        {
            movingRight = !movingRight;
            changeSide = 7;
        }
    }
}
