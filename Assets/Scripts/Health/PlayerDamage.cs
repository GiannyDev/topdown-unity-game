using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private int damageToApply = 1;
    
    private Health playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            playerHealth.TakeDamage(damageToApply);
        }
    }
}
