using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Name")] 
    [SerializeField] private string weaponName = "";

    [Header("Settings")] 
    [SerializeField] private float timeBtwShots = 0.5f;

    [Header("Weapon")] 
    [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool autoReload = true;

    [Header("Recoil")] 
    [SerializeField] private bool useRecoil = true;
    [SerializeField] private int recoilForce = 5;

    [Header("Effects")] 
    [SerializeField] private ParticleSystem muzzlePS;

    /// <summary>
    /// Returns the name of this Weapon
    /// </summary>
    public string WeaponName => weaponName;

    /// <summary>
    /// Reference of the Character that controls this Weapon
    /// </summary>
    public Character WeaponOwner { get; set; }

    /// <summary>
    /// Current Ammo we have
    /// </summary>
    public int CurrentAmmo { get; set; }

    /// <summary>
    /// Reference to our WeaponAmmo
    /// </summary>
    public WeaponAmmo WeaponAmmo { get; set; }

    /// <summary>
    /// Returns the aim of this weapon
    /// </summary>
    public WeaponAim WeaponAim { get; set; }
    
    /// <summary>
    /// Returns if this weapon use magazine
    /// </summary>
    public bool UseMagazine => useMagazine;

    /// <summary>
    /// Returns the size of our Magazine
    /// </summary>
    public int MagazineSize => magazineSize;

    /// <summary>
    /// Returns if we can shoot now
    /// </summary>
    public bool CanShoot { get; set; }

    /// <summary>
    /// Returns if the weapon was created before
    /// </summary>
    public bool WeaponCreated
    {
        get => PlayerPrefs.GetInt(WeaponName, 1) == 1;
        set => PlayerPrefs.SetInt(WeaponName, value ? 1 : 0);
    }

    /// <summary>
    /// 
    /// </summary>
    public ObjectPooler WeaponPooler { get; set; } // ---- NEW
    
    // Internal
    private float nextShotTime;
    private CharacterController controller;
    private Animator animator;
    private readonly int weaponUseParameter = Animator.StringToHash("WeaponUse");

    protected virtual void Awake()
    {
        WeaponAmmo = GetComponent<WeaponAmmo>();
        WeaponAim = GetComponent<WeaponAim>();
        animator = GetComponent<Animator>();
        WeaponPooler = GetComponent<ObjectPooler>();  // ---- NEW
    }

    protected virtual void Update()
    {
        WeaponCanShoot();
        RotateWeapon();
    }

    /// <summary>
    /// Trigger our Weapon in order to use it
    /// </summary>
    public virtual void UseWeapon()
    {
        StartShooting();
    }

    /// <summary>
    /// Makes our Weapon stop working
    /// </summary>
    public void StopWeapon()
    {
        if (useRecoil)
        {
            controller.ApplyRecoil(Vector2.one, 0f);
        }
    }

    /// <summary>
    /// Activates our weapon in order to shoot
    /// </summary>
    private void StartShooting()
    {
        if (useMagazine)
        {
            if (WeaponAmmo != null)
            {
                if (WeaponAmmo.CanUseWeapon())
                {
                    RequestShot();
                }
                else
                {
                    if (autoReload)
                    {
                        Reload();
                    }
                }
            }
        }
        else
        {
            RequestShot();
        }
    }

    /// <summary>
    /// Makes our weapon start shooting
    /// </summary>
    protected virtual void RequestShot()
    {
        if (!CanShoot)
        {
            return;
        }

        if (useRecoil)
        {
            Recoil();
        }

        animator.SetTrigger(weaponUseParameter);
        WeaponAmmo.ConsumeAmmo();
        muzzlePS.Play();
    }

    /// <summary>
    /// Apply a force to our movement when we shoot
    /// </summary>
    private void Recoil()
    {
        if (WeaponOwner != null)
        {
            if (WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
            {
                controller.ApplyRecoil(Vector2.left, recoilForce);
            }
            else
            {
                controller.ApplyRecoil(Vector2.right, recoilForce);
            }
        }
    }

    /// <summary>
    /// Controls the next time we can shoot
    /// </summary>
    protected virtual void WeaponCanShoot()
    {
        if (Time.time > nextShotTime)
        {
            CanShoot = true;
            nextShotTime = Time.time + timeBtwShots;
        }
    }

    /// <summary>
    /// Reference the owner of this Weapon
    /// </summary>
    /// <param name="owner">The Character</param>
    public void SetOwner(Character owner)
    {
        WeaponOwner = owner;
        controller = WeaponOwner.GetComponent<CharacterController>();
    }

    /// <summary>
    /// Reload our Weapon
    /// </summary>
    public void Reload()
    {
        if (WeaponAmmo != null)
        {
            if (useMagazine)
            {
                WeaponAmmo.RefillAmmo();
            }
        }
    }

    protected virtual void RotateWeapon()
    {
        if (WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}