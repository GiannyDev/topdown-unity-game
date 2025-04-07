using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCirclePattern : BossBaseShot
{
    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (isShooting == false)
        {
            return;
        }

        float shiftAngle = 360f / projectileAmount;
        for (int i = 0; i < projectileAmount; i++)
        {
            BossProjectile bossProjectile = GetBossProjectile(transform.position);
            if (bossProjectile == null)
            {
                break;
            }

            float angle = shiftAngle * i;
            ShootProjectile(bossProjectile, projectileSpeed, angle, projectileAcceleration);
        }
        
        DisableShooting();
    }

    public void EnableProjectile()
    {
        isShooting = true;
    }
}
