using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject fireProjectile;
    [SerializeField]
    private float fireProjectileVelocity;
    private readonly FireProjectileController fireProjectileController;
    [SerializeField]
    private GameObject BigFireProjectile;

    public GameObject GetFireProjectile(bool direction, string hittableColliderTag)
    {
        //True to shoots to right. False to shoots to left
        GameObject newFireProjectile = Instantiate(fireProjectile);
        newFireProjectile.GetComponent<FireProjectileController>().FireProjectileInitializer(direction, hittableColliderTag , fireProjectileVelocity);
        return newFireProjectile;
    }

    public GameObject GetBigFireProjectile(float angle, string hittableColliderTag)
    {
        //True to shoots to right. False to shoots to left
        GameObject newFireProjectile = Instantiate(BigFireProjectile);
        newFireProjectile.GetComponent<FireProjectileController>().BigFireProjectileInitializer(angle, hittableColliderTag, fireProjectileVelocity);
        return newFireProjectile;
    }

    public static void GetIceProjectile()
    {

    }
}
