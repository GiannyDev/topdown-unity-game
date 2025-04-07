using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossRandomPattern : BossBaseShot
{
    [Header("Random Settings")]
    [Range(0f, 360f)][SerializeField] private float startAngle = 180f;
    [Range(0f, 360f)][SerializeField] private float range = 360f;
    
    [SerializeField] private float minRandomSpeed = 1f;
    [SerializeField] private float maxRandomSpeed = 3f;

    [SerializeField] private float minDelay = 0.01f;
    [SerializeField] private float maxDelay = 0.1f;

    private float nextShotTime;
    private int shotIndex;
    
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

        BossProjectile bossProjectile = GetBossProjectile(transform.position);
        if (bossProjectile == null)
        {
            return;
        }

        // Get random Speed
        float speed = Random.Range(minRandomSpeed, maxRandomSpeed);

        // Get random angle
        float minAngle = startAngle - range / 2f;
        float maxAngle = startAngle + range / 2f;
        float angle = Random.Range(minAngle, maxAngle);
        
        // Shoot
        ShootProjectile(bossProjectile, speed, angle, projectileAcceleration);

        // Increase index
        shotIndex++;
        
        // Stop shooting if all the projectiles are used
        if (shotIndex >= projectileAmount)
        {
            DisableShooting();
        }
        else
        {
            // Update time to Shoot
            nextShotTime = Time.time + Random.Range(minDelay, maxDelay);
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
