using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int health;
    private bool invulnerable = false;
    public Animator animator;
    public HealthBar healthBar;
    private PlayerController playerController;
    Renderer render;
    Color c;

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        playerController = gameObject.GetComponent<PlayerController>();
        render = GetComponent<Renderer>();
        c = render.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Die();
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        healthBar.SetHealth(this.health);
        if(playerController.getDirection())
            transform.position += Vector3.left * 30f * Time.deltaTime;
        else
            transform.position += Vector3.right * 30f * Time.deltaTime;
        transform.position += Vector3.up * 30f * Time.deltaTime;
        StartCoroutine("Invulnerability");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            TakeDamage(2);

        if (collision.collider.CompareTag("HealthItem"))
        {
            health = maxHealth;
            healthBar.SetHealth(this.health);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireProjectile"))
            TakeDamage(2);
    }

    IEnumerator Invulnerability()
    {
        invulnerable = true;
        c.a = 0.5f;
        render.material.color = c;
        Physics2D.IgnoreLayerCollision(11, 9, true);
        Physics2D.IgnoreLayerCollision(11, 12, true);
        yield return new WaitForSeconds(1.5f);
        Physics2D.IgnoreLayerCollision(11, 9, false);
        Physics2D.IgnoreLayerCollision(11, 12, false);
        invulnerable = false;
        c.a = 1f;
        render.material.color = c;
    }

    private void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
        print("Died");
    }
}
