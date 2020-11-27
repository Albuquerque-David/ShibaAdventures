using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private bool isFireballEnabled = false;

    private ShootFactory shootFactory;
    private PlayerController playerController;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        shootFactory = gameObject.GetComponent<ShootFactory>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isFireballEnabled)
            Shoot();
        if (Input.GetKeyDown(KeyCode.C))
            MeeleAttack();
    }

    void Shoot()
    {
        if(playerController.getDirection())
        {

            GameObject newShoot = shootFactory.GetFireProjectile(true, "Enemy");
            Vector3 newPosition = transform.position;
            newPosition.y += 0.4f;
            newPosition.x += 1f;
            newShoot.transform.position = newPosition;
        }
        else
        {
            GameObject newShoot = shootFactory.GetFireProjectile(false, "Enemy");
            Vector3 newPosition = transform.position;
            newPosition.y += 0.4f;
            newPosition.x -= 1f;
            newShoot.transform.position = newPosition;
        }

        animator.SetBool("Jumping", false);
        animator.SetBool("ShootAttacking", true);
        animator.SetTrigger("ShootAttack");

    }

    void MeeleAttack()
    {
        animator.SetBool("Jumping", false);
        animator.SetBool("MeeleAttacking", true);
        animator.SetTrigger("MeeleAttack");


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.TakeDamage(2);
            
        }
        
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("FireballItem"))
        {
            isFireballEnabled = true;
            Destroy(collision.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
