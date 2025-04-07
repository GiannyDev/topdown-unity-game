using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeapon : Colletables
{
    [SerializeField] private ItemData itemWeaponData;

    protected override void Pick()
    {
        EquipWeapon();
    }

    private void EquipWeapon()
    {
        if (character != null)
        {
            character.GetComponent<CharacterWeapon>().SecondaryWeapon = itemWeaponData.WeaponToEquip;
        }
    }
    
    protected override void PlayEffects()
    {
        //SoundManager.Instance.PlaySound(SoundManager.Instance.ItemClip, 0.6f);
    }
}
