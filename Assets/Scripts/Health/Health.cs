using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Action OnBossDead;
    
    [Header("Health")]
    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    [Header("Shield")] 
    [SerializeField] private float initialShield = 5f;
    [SerializeField] private float maxShield = 5f;
    
    [Header("Settings")] 
    [SerializeField] private bool destroyObject;

    private Character character;
    private CharacterController controller;
    private Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;
    private BossBaseShot bossBaseShot;
    
    private bool isPlayer;
    private bool shieldBroken;
    
    /// <summary>
    /// Controls the current health of the object
    /// </summary>
    public float CurrentHealth { get; set; }

    /// <summary>
    /// Returns the current health of this character
    /// </summary>
    public float CurrentShield { get; set; }
    
    private void Awake()
    {
        character = GetComponent<Character>();
        controller = GetComponent<CharacterController>();
        collider2D = GetComponent<Collider2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        bossBaseShot = GetComponent<BossBaseShot>();
        
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        CurrentHealth = initialHealth;
        CurrentShield = initialShield;

        if (character != null)
        {
            isPlayer = character.CharacterType == Character.CharacterTypes.Player;
        }
        
        UpdateCharacterHealth();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }
    }

    /// <summary>
    /// Take the amount of damage we pass in parameters
    /// </summary>
    /// <param name="damage">Amount to damage</param>
    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0)
        {
            return;
        }

        if (!shieldBroken && character != null && initialShield > 0)
        {
            CurrentShield -= damage;
            UpdateCharacterHealth();
            if (CurrentShield <= 0)
            {
                shieldBroken = true;
            }
            return;
        }
        
        CurrentHealth -= damage;
        UpdateCharacterHealth();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Kills our object
    /// </summary>
    private void Die()
    {
        if (character != null)
        {
            collider2D.enabled = false;
            spriteRenderer.enabled = false;

            character.enabled = false;
            controller.enabled = false;
        }

        if (bossBaseShot != null)
        {
            OnBossDead?.Invoke();
        }
        
        if (destroyObject)
        {
            DestroyObject();
        }
    }

    /// <summary>
    /// Revive this object
    /// </summary>
    public void Revive()
    {
        if (character != null)
        {
            collider2D.enabled = true;
            spriteRenderer.enabled = true;
            
            character.enabled = true;
            controller.enabled = true;
        }
        
        gameObject.SetActive(true);

        CurrentHealth = initialHealth;
        CurrentShield = initialShield;
        
        UpdateCharacterHealth();
    }

    public void GainHealth(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        UpdateCharacterHealth();
    }
    
    public void GainShield(int amount)
    {
        CurrentShield = Mathf.Min(CurrentShield + amount, maxShield);
        UpdateCharacterHealth();
    }
    
    /// <summary>
    /// If destroyObject is selected, we destroy this object
    /// </summary>
    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }

    private void UpdateCharacterHealth()
    {
        // Update Enemy health
        if (enemyHealth != null && bossBaseShot == null)
        {
            enemyHealth.UpdateEnemyHealth(CurrentHealth, maxHealth);
        }

        // Update Boss health
        if (bossBaseShot != null && character.CharacterType == Character.CharacterTypes.AI)
        {
            UIManager.Instance.UpdateBossHealth(CurrentHealth, maxHealth);
        }
        
        // Update Player health
        if (character != null && bossBaseShot == null && character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth, CurrentShield, maxShield, isPlayer);
        }
    }
}