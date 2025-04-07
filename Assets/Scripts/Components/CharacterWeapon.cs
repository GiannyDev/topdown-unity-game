using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponents
{
    public static Action OnStartShooting;
    
    [Header("Weapon Settings")]
    [SerializeField] private Weapon weaponToUse;
    [SerializeField] private Transform weaponHolderPosition;

    // Dictionary where we store the weapons we created
    private Dictionary<string, Weapon> _weaponsCreated = new Dictionary<string, Weapon>();
    
    /// <summary>
    /// Reference of the Weapon we are using
    /// </summary>
    public Weapon CurrentWeapon  { get; set; }

    /// <summary>
    /// Store the reference to the second weapon
    /// </summary>
    public Weapon SecondaryWeapon { get; set; }
    
    /// <summary>
    /// Returns the reference to our Current Weapon Aim
    /// </summary>
    public WeaponAim WeaponAim { get; set; }
    
    protected override void Start()
    {
        base.Start();
        EquipWeapon(weaponToUse, weaponHolderPosition);
    }

    protected override void HandleInput()
    {
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopWeapon();    
            }
        
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && SecondaryWeapon != null)
            {
                EquipWeapon(weaponToUse, weaponHolderPosition);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && SecondaryWeapon != null)
            {
                EquipWeapon(SecondaryWeapon, weaponHolderPosition);
            }
        }
    }

    /// <summary>
    /// Fire our Weapon
    /// </summary>
    public void Shoot()
    {
        if (CurrentWeapon == null)
        {
            return;
        }
        
        CurrentWeapon.UseWeapon();
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            OnStartShooting?.Invoke();
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }

    /// <summary>
    /// When we stop shooting we stop using our Weapon
    /// </summary>
    public void StopWeapon()
    {
        if (CurrentWeapon == null)
        {
            return;
        }
        
        CurrentWeapon.StopWeapon();
    }
    
    /// <summary>
    /// Reload our Weapon
    /// </summary>
    public void Reload()
    {
        if (CurrentWeapon == null)
        {
            return;
        }
        
        CurrentWeapon.Reload();
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }

    /// <summary>
    /// Equip the weapon we specify
    /// </summary>
    /// <param name="weapon">Weapon to use</param>
    /// <param name="weaponPosition">Where to Instantiate our weapon</param>
    public void EquipWeapon(Weapon weapon, Transform weaponPosition)
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.WeaponAmmo.SaveAmmo();
            WeaponAim.DestroyReticle();
            // Destroy(GameObject.Find("Pool")); // --- DELETE THIS
            Destroy(CurrentWeapon.WeaponPooler.PoolContainer); // ---- NEW
            Destroy(CurrentWeapon.gameObject);
        }
        
        CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        CurrentWeapon.transform.parent = weaponPosition;
        CurrentWeapon.SetOwner(character);
        WeaponAim = CurrentWeapon.GetComponent<WeaponAim>();

        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            // Add weapon created
            if (!_weaponsCreated.ContainsKey(CurrentWeapon.WeaponName))
            {
                _weaponsCreated.Add(CurrentWeapon.WeaponName, CurrentWeapon);
            }
        
            // Then we load the weapon ammo, this depends if the weapon was created for the first time or not.
            if (!CurrentWeapon.WeaponCreated)
            {
                CurrentWeapon.WeaponAmmo.RefillAmmo();
            }
            else // Pero sí ya fue creado anteriormente, cargamos la municion guardada.
            {
                CurrentWeapon.WeaponAmmo.LoadWeaponMagazineSize();;
            }
        
            // Then we save the value that this weapon was created (So next time we chaange weapons, we load the current ammo)
            if (!CurrentWeapon.WeaponCreated)
            {
                CurrentWeapon.WeaponCreated = true;
            }
        }
        
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
            UIManager.Instance.UpdateWeaponSprite(CurrentWeapon.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        }
    }
    
    private void OnDisable()
    {
        // Delete save data
        foreach (Weapon weapon in _weaponsCreated.Values)
        {
            weapon.WeaponCreated = false;
        }
    }
}