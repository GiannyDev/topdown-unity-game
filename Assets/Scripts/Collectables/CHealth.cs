using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHealth : Colletables
{
    [SerializeField] private int healthToAdd = 1;
    [SerializeField] private ParticleSystem healthBonus;
    
    protected override void Pick()
    {
        AddHealth(character);    
    }

    protected override void PlayEffects()
    {
        Instantiate(healthBonus, transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(SoundManager.Instance.ItemClip, 0.6f);
    }

    public void AddHealth(Character character)
    {
        character.GetComponent<Health>().GainHealth(healthToAdd);
    }
}
