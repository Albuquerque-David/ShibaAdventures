using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowIA : MonoBehaviour
{
    // Start is called before the first frame update
    private bool movimentDirection;
    private bool isMoving;
    public Transform enemyArea;
    public float enemyAreaRange = 0.5f;
    public LayerMask playerLayer;
    [SerializeField]
    private float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Moviment();
        }
        else
            checkIfCanStartMoving();
    }

    private void checkIfCanStartMoving()
    {
        Collider2D enemyStartMoving = Physics2D.OverlapCircle(enemyArea.position, enemyAreaRange, playerLayer);
        if (enemyStartMoving != null)
            isMoving = true;
    }

    private void Moviment()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CheckDirection();
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * 0.5f * Time.deltaTime);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool CheckDirection()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = transform.InverseTransformPoint(player.transform.position);
        if (direction.x > 0)
        {
            Flip();
        }

        Vector3 scale = transform.localScale;
        if (scale.x < 0)
            return true;
        else
            return false;
    }

    void OnDrawGizmosSelected()
    {
        if (enemyArea == null)
            return;

        Gizmos.DrawWireSphere(enemyArea.position, enemyAreaRange);
    }
}
