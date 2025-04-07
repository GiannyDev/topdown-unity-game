using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseShot : MonoBehaviour
{
    [Header("Projectile Settings")] 
    [SerializeField] protected int projectileAmount = 20;
    [SerializeField] protected float projectileSpeed = 2f;
    [SerializeField] protected float projectileAcceleration = 0f;
    
    protected ObjectPooler pooler;
    protected bool isShooting;
    
    protected virtual void Start()
    {
        pooler = GetComponent<ObjectPooler>();
    }

    protected BossProjectile GetBossProjectile(Vector3 newPosition)
    {
        GameObject bossProjectilePooled = pooler.GetObjectFromPool();
        BossProjectile bossProjectile = bossProjectilePooled.GetComponent<BossProjectile>();

        bossProjectile.transform.position = newPosition;
        bossProjectilePooled.SetActive(true);
        bossProjectile.EnableBossProjectile();

        return bossProjectile;
    }

    protected void ShootProjectile(BossProjectile bossProjectile, float speed, float angle, float acceleration)
    {
        if (bossProjectile == null)
        {
            return;
        }
        
        bossProjectile.Shoot(angle, speed, acceleration);
    }
    
    protected virtual void EnableShooting()
    {
        isShooting = true;
    }
    
    protected virtual void DisableShooting()
    {
        isShooting = false;
    }
}
