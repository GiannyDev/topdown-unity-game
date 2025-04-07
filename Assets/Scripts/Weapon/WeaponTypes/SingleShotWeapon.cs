using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SingleShotWeapon : Weapon
{
    [SerializeField] private Vector3 projectileSpawnPosition;
    [SerializeField] private Vector3 projectileSpread;
    
    /// <summary>
    /// Controls the position of our projectile spawn
    /// </summary>
    public Vector3 ProjectileSpawnPosition { get; set; }

    /// <summary>
    /// Returns the reference to the pooler in this GameObject
    /// </summary>
    public ObjectPooler Pooler { get; set; }

    private Vector3 projectileSpawnValue;
    private Vector3 randomProjectileSpread;

    protected override void Awake()
    {
        base.Awake();
        
        projectileSpawnValue = projectileSpawnPosition;
        projectileSpawnValue.y = -projectileSpawnPosition.y;

        Pooler = GetComponent<ObjectPooler>();
    }
    
    protected override void RequestShot()
    {
        base.RequestShot();

        if (CanShoot)
        {
            EvaluateProjectileSpawnPosition();
            SpawnProjectile(ProjectileSpawnPosition);
        }
    }

    /// <summary>
    /// Spawns a projectile from the pool, setting it's new direction based on the character's direction (WeaponOwner)
    /// </summary>
    /// <param name="spawnPosition">Where the projectile gets fired</param>
    private void SpawnProjectile(Vector2 spawnPosition)
    {
        // Get Object from the pool
        GameObject projectilePooled = Pooler.GetObjectFromPool();
        projectilePooled.transform.position = spawnPosition;
        projectilePooled.SetActive(true);

        // Get reference to the projectile
        Projectile projectile = projectilePooled.GetComponent<Projectile>();
        projectile.EnableProjectile();
        projectile.ProjectileOwner = WeaponOwner;

        // Spread
        randomProjectileSpread.z = Random.Range(-projectileSpread.z, projectileSpread.z);
        Quaternion spread = Quaternion.Euler(randomProjectileSpread);

        // Set direction and rotation
        Vector2 newDirection = WeaponOwner.GetComponent<CharacterFlip>().FacingRight ? spread * transform.right : spread * transform.right * -1;
        projectile.SetDirection(newDirection, transform.rotation, WeaponOwner.GetComponent<CharacterFlip>().FacingRight);

        //SoundManager.Instance.PlaySound(SoundManager.Instance.ShootClip, 0.8f);
        
        // Disable shot after next shot time
        CanShoot = false;
    }

    /// <summary>
    /// Calculates the position where our projectile is going to be fired
    /// </summary>
    private void EvaluateProjectileSpawnPosition()
    {
        if (WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
        {
            // Right side
            ProjectileSpawnPosition = transform.position + transform.rotation * projectileSpawnPosition;
        }
        else
        {
            // Left side
            ProjectileSpawnPosition = transform.position - transform.rotation * projectileSpawnValue;
        }
    }

    private void OnDrawGizmosSelected()
    {
        projectileSpawnValue = projectileSpawnPosition;
        projectileSpawnValue.y = -projectileSpawnPosition.y;
        
        if (transform.localScale == Vector3.one)
        {
            ProjectileSpawnPosition = transform.position + transform.rotation * projectileSpawnPosition;
        }
        else
        {
            ProjectileSpawnPosition = transform.position - transform.rotation * projectileSpawnValue;
        }
        
        // EvaluateProjectileSpawnPosition();

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ProjectileSpawnPosition, 0.1f);
    }
}