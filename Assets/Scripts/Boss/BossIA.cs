using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour
{
    public float attackRange;
    public Transform attackPoint;
    public LayerMask playerLayer;

    public Transform rightPosition;
    public Transform leftPosition;

    private ShootFactory shootFactory;
    private EnemyController enemyController;
    private Player player;

    public GameObject kingdomKey;

    void Start()
    {
        shootFactory = gameObject.GetComponent<ShootFactory>();
        enemyController = gameObject.GetComponent<EnemyController>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (!enemyController.getIsAlive())
        {
            InstantiateKingdomKey();
            player.setCheemsKey(true);
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void FlipToLeft()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        if (scale.x > 0)
            scale.x *= -1;
        transform.localScale = scale;
    }

    public void FlipToRight()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        if (scale.x < 0)
            scale.x *= -1;
        transform.localScale = scale;
    }

    public void CheckDirection()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = transform.InverseTransformPoint(player.transform.position);
        if (direction.x < 0)
        {
            Flip();
        }
    }

    public bool GetDirection()
    {
        Vector3 scale = transform.localScale;
        if (scale.x < 0)
            return false;
        else
            return true;
    }

    public void MeeleAttack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (hitPlayer == null)
            return;
        PlayerHealth playerHealth = hitPlayer.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(2);
    }

    public void MeeleAttack2()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange * 1.3f, playerLayer);
        if (hitPlayer == null)
            return;
        PlayerHealth playerHealth = hitPlayer.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(2);
    }

    public void FireAttack1()
    {
        int angle = Random.Range(45, 81);

        if (GetDirection())
        {
            GameObject newShoot = shootFactory.GetBigFireProjectile(angle, "Player");
            Vector3 newPosition = transform.position;
            newPosition.y += 2.5f;
            newPosition.x += 2.5f;
            newShoot.transform.position = newPosition;
        }
        else
        {
            GameObject newShoot = shootFactory.GetBigFireProjectile((angle)*-1, "Player");
            Vector3 newPosition = transform.position;
            newPosition.y += 2.5f;
            newPosition.x -= 2.5f;
            newShoot.transform.position = newPosition;
        }
    }

    void InstantiateKingdomKey()
    {
        GameObject newKingdomKey = Instantiate(kingdomKey);
        Transform newTransf = enemyController.getDeathPosition();
        Vector2 newPos = new Vector2(newTransf.position.x, newTransf.position.y);
        newKingdomKey.transform.position = newPos;
        newKingdomKey.transform.position += Vector3.up * 30f * Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange * 1.3f);
    }


}
