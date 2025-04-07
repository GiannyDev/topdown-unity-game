using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShield : Colletables
{
    [SerializeField] private int shieldToAdd = 1;
    [SerializeField] private ParticleSystem shieldEffect;

    protected override void Pick()
    {
        AddShield(character);
    }

    protected override void PlayEffects()
    {
        Instantiate(shieldEffect, transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(SoundManager.Instance.ItemClip, 0.6f);
    }

    public void AddShield(Character characterHealth)
    {
        characterHealth.GetComponent<Health>().GainShield(shieldToAdd);
    }
}