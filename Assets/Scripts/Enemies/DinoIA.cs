using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DinoIA : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool movimentDirection;
    public Transform enemyArea;
    public Transform enemyStartMoving;
    public float enemyAreaRange = 0.5f;
    private bool attackMode = false;
    public LayerMask playerLayer;
    private float tempTime;

    private bool isMoving = false;
    [SerializeField]
    private bool freeze = false;

    private ShootFactory shootFactory;

    void Start()
    {
        shootFactory = gameObject.GetComponent<ShootFactory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            if (!attackMode)
            {
                if(!freeze)
                    Moviment(movimentDirection);
                CheckPlayerInEnemyArea();
            }
            else
            {
                tempTime += Time.deltaTime;
                if (tempTime > 1.5)
                {
                    tempTime = 0;
                    ShootFireball();
                }
            }
        }
        else
        {
            checkIfCanStartMoving();
        }
            
    }

    private void checkIfCanStartMoving()
    {
        Collider2D enemyStartMoving = Physics2D.OverlapCircle(enemyArea.position, enemyAreaRange * 2, playerLayer);
        if (enemyStartMoving != null)
            isMoving = true;
    }

    private void CheckPlayerInEnemyArea()
    {
        Collider2D enemyAreaCol = Physics2D.OverlapCircle(enemyArea.position, enemyAreaRange, playerLayer);
        if (enemyAreaCol != null)
        {
            attackMode = true;
        }
        else
        {
            attackMode = false;
        }
    }

    private void Moviment(bool movimentDirection)
    {
        if(movimentDirection)
            transform.position += Vector3.right * 0.5f * Time.deltaTime;
        else
            transform.position += Vector3.left * 0.5f * Time.deltaTime;
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

    void ShootFireball()
    {
        if (CheckDirection())
        {
            GameObject newShoot = shootFactory.GetFireProjectile(true, "Player");
            Vector3 newPosition = transform.position;
            newPosition.y += 0f;
            newPosition.x += 1f;
            newShoot.transform.position = newPosition;
        }
        else
        {
            GameObject newShoot = shootFactory.GetFireProjectile(false, "Player");
            Vector3 newPosition = transform.position;
            newPosition.y += 0f;
            newPosition.x -= 1f;
            newShoot.transform.position = newPosition;
        }

    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    void OnDrawGizmosSelected()
    {
        if (enemyArea == null)
            return;

        Gizmos.DrawWireSphere(enemyArea.position, enemyAreaRange);
        Gizmos.DrawWireSphere(enemyArea.position, enemyAreaRange * 2);
    }

}
