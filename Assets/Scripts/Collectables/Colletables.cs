using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colletables : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool canDestroyItem = true;
    
    protected Character character;
    protected GameObject objectCollided;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D collider2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectCollided = other.gameObject;
        if (IsPickable())
        {
            Pick();
            PlayEffects();

            if (canDestroyItem)
            {
                Destroy(gameObject);
            }
            else
            {
                spriteRenderer.enabled = false;
                collider2D.enabled = false;
            }
        }
    }

    protected virtual bool IsPickable()
    {
        character = objectCollided.GetComponent<Character>();
        if (character == null)
        {
            return false;
        }

        return character.CharacterType == Character.CharacterTypes.Player;

        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            // Regresamos verdadero, hemos colisionado con el Player
            return true;
        }
        else
        {
            // Regresamos falso, hemos colisionado con el Enemy. No hacemos nada
            return false;
        }
    }

    protected virtual void Pick()
    {
        // ---
    }

    protected virtual void PlayEffects()
    {
        // -------        
    }
}
