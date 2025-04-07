using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask objectMask;
    [SerializeField] private float lifeTime = 2f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem impactPS;
    
    private Projectile projectile;
    private BossProjectile bossProjectile;

    private void Start()
    {
        projectile = GetComponent<Projectile>();
        bossProjectile = GetComponent<BossProjectile>();
    }

    /// <summary>
    /// Returns this object to the pool
    /// </summary>
    private void Return()
    {
        if (projectile != null)
        {
            projectile.ResetProjectile();
        }
        
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (MyLibrary.CheckLayer(other.gameObject.layer, objectMask))
        {
            if (projectile != null)
            {
                projectile.DisableProjectile();
            }
            
            if (bossProjectile != null)
            {
                bossProjectile.DisableBossProjectile();
            }
            
            SoundManager.Instance.PlaySound(SoundManager.Instance.ImpactClip, 0.1f);
            impactPS.Play();
            Invoke(nameof(Return), impactPS.main.duration);
        }
    }
    
    private void OnEnable()
    {
        if (lifeTime > 0)
        {
            Invoke(nameof(Return), lifeTime);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}