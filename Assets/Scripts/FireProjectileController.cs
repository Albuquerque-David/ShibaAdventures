using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FireProjectileController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isBigFireProjectile = false;
    private bool direction;
    private float horizontalVelocity = 0.5f;
    private bool isInitialized = false;
    private string hittableColliderTag;
    private float angle;

    private float tempTime;


    public void FireProjectileInitializer(bool direction, string hittableColliderTag = "Enemy", float horizontalVelocity = 0.5f)
    {
        //True shoots to the right. False shoots to the left
        this.direction = direction;
        this.horizontalVelocity = horizontalVelocity;
        this.hittableColliderTag = hittableColliderTag;
        this.isInitialized = true;
    }

    public void BigFireProjectileInitializer(float angle, string hittableColliderTag = "Enemy", float horizontalVelocity = 0.5f)
    {
        //True shoots to the right. False shoots to the left
        this.isBigFireProjectile = true;
        this.angle = angle;
        this.horizontalVelocity = horizontalVelocity;
        this.hittableColliderTag = hittableColliderTag;
        this.isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInitialized)
        {
            if (!isBigFireProjectile)
                Move();
            else
                BigFireProjectileMove();
        }

        tempTime += Time.deltaTime;
        if (tempTime > 10)
        {
            Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        if(direction)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.position += Vector3.right * horizontalVelocity * Time.deltaTime;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            transform.position += Vector3.left * horizontalVelocity * Time.deltaTime;
        }
    }

    private void BigFireProjectileMove()
    {
        //Choose an angle between 45 and 90
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if(angle < 0)
        {
            Vector3 transformAngle = new Vector3(angle, angle, 0);
            transform.position += transformAngle * horizontalVelocity / 10 * Time.deltaTime;
        }
        else
        {
            Vector3 transformAngle = new Vector3(-angle, angle, 0);
            transform.position -= transformAngle * horizontalVelocity / 10 * Time.deltaTime;
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(hittableColliderTag))
            Destroy(gameObject);

    }

    public string getHittableColliderTag()
    {
        return hittableColliderTag;
    }
}
