using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpiralPattern : BossBaseShot
{
    [Range(0f, 360f)][SerializeField] private float startAngle = 180f;
    [Range(-360f, 360f)][SerializeField] private float shiftAngle = 5f;
    [SerializeField] private float shotDelay = 0.5f;

    private int shotIndex;
    private float nextShotTime;

    private void Update()
    {
        nextShotTime = 0f;
        Shoot();
    }

    private void Shoot()
    {
        if (isShooting == false)
        {
            return;
        }

        // This adds an extra check to only run the code below in a fixed time
        if (nextShotTime >= 0f)
        {
            nextShotTime -= Time.deltaTime;
            if (nextShotTime >= 0f)
            {
                return;
            }
        }

        // Get projectile
        BossProjectile bossProjectile = GetBossProjectile(transform.position);

        // Get angle
        float angle = startAngle + shiftAngle * shotIndex;
        
        // Shoot
        ShootProjectile(bossProjectile, projectileSpeed, angle, projectileAcceleration);
        
        // Increase Shoot index
        shotIndex++;

        // Stop shooting if all the projectiles are used
        if (shotIndex >= projectileAmount)
        {
            DisableShooting();
        }
        else
        {
            // Update time to Shoot
            nextShotTime = Time.time + shotDelay;
            if (nextShotTime <= 0)
            {
                Update();
            }
        }
    }

    public void EnableProjectile()
    {
        isShooting = true;
        shotIndex = 0;
    }
}
