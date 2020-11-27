using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int maxLife;
    [SerializeField]
    private int life;
    private bool isAlive = true;
    Color c;
    Renderer render;
    private Transform deathPosition;
    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
        render = GetComponent<Renderer>();
        c = render.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(life == 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireProjectile"))
        {
            FireProjectileController fireProjectileController = collision.GetComponent<FireProjectileController>();
            if(fireProjectileController.getHittableColliderTag().Equals("Enemy"))
                TakeDamage(2);
        }

    }

    public void TakeDamage(int damage)
    {
        this.life -= damage;
        StartCoroutine("HitEffect");
    }

    IEnumerator HitEffect()
    {
        c.a = 0.5f;
        render.material.color = c;
        yield return new WaitForSeconds(0.2f);
        c.a = 1f;
        render.material.color = c;   
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void Die()
    {
        BossIA boss = gameObject.GetComponent<BossIA>();
        isAlive = false;
        deathPosition = gameObject.transform;
        if(boss == null)
            DestroyGameObject();
    }

    public Transform getDeathPosition()
    {
        return deathPosition;
    }

    public bool getIsAlive()
    {
        return isAlive;
    }

    public int getHealth()
    {
        return this.life;
    }

    public int getMaxHealth()
    {
        return this.maxLife;
    }

}
