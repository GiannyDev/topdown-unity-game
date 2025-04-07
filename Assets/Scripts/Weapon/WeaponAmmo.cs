using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    private Weapon weapon;

    private readonly string WEAPON_AMMO_SAVELOAD = "Weapon_";
    
    private void Awake()
    {
        weapon = GetComponent<Weapon>();
        // RefillAmmo(); // --- Dont use this
        // LoadWeaponMagazineSize(); // --- Dont use this
    }

    /// <summary>
    /// Consume our ammo when we shoot
    /// </summary>
    public void ConsumeAmmo()
    {
        if (weapon.UseMagazine)
        {
            weapon.CurrentAmmo -= 1;
        }
    }

    /// <summary>
    /// Returns true if we have if we have enough ammo
    /// </summary>
    /// <returns></returns>
    public bool CanUseWeapon()
    {
        if (weapon.CurrentAmmo > 0)
        {
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Refills our wepaon with ammo
    /// </summary>
    public void RefillAmmo()
    {
        if (weapon.UseMagazine)
        {
            weapon.CurrentAmmo = weapon.MagazineSize;
        }
    }

    public void LoadWeaponMagazineSize()
    {
        int savedAmmo = LoadAmmo();
        weapon.CurrentAmmo = savedAmmo < weapon.MagazineSize ? LoadAmmo() : weapon.MagazineSize;
    }
    
    public void SaveAmmo()
    {
        PlayerPrefs.SetInt(WEAPON_AMMO_SAVELOAD + weapon.WeaponName, weapon.CurrentAmmo);
    }

    public int LoadAmmo()
    {
        return PlayerPrefs.GetInt(WEAPON_AMMO_SAVELOAD + weapon.WeaponName, weapon.MagazineSize);
    }
}
