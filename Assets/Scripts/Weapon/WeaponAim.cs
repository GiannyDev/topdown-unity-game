using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private GameObject reticlePrefab;

    /// <summary>
    /// Returns the current absolute angle
    /// </summary>
    public float CurrentAimAngleAbsolute { get; set; }
    
    /// <summary>
    /// Returns the current angle
    /// </summary>
    public float CurrentAimAngle { get; set; }
    
    private Camera mainCamera;
    private GameObject reticle;
    private Weapon weapon;

    private Vector3 direction;
    private Vector3 mousePosition;
    private Vector3 reticlePosition;
    private Vector3 currentAim = Vector3.zero;
    private Vector3 currentAimAbsolute = Vector3.zero;
    private Quaternion initialRotation;
    private Quaternion lookRotation;

    private void Start()
    {
        Cursor.visible = false;
        weapon = GetComponent<Weapon>();
        initialRotation = transform.rotation;
        
        mainCamera = Camera.main;
        reticle = Instantiate(reticlePrefab);
    }

    private void Update()
    {
        if (weapon.WeaponOwner.CharacterType == Character.CharacterTypes.Player)
        {
            GetMousePosition();
        }
        else
        {
            EnemyAim();
        }
        
        RotateWeapon();
        MoveReticle();
    }
    
    /// <summary>
    /// Get the exact mouse position in order to aim
    /// </summary>
    private void GetMousePosition()
    {
        // Get Mouse Position
        mousePosition = Input.mousePosition;
        mousePosition.z = 5f;

        // Get World space position
        direction = mainCamera.ScreenToWorldPoint(mousePosition);
        direction.z = transform.position.z;
        reticlePosition = direction;

        currentAimAbsolute = direction - transform.position;
        if (weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
        {
            currentAim = direction - transform.position;
        }
        else
        {
            currentAim = transform.position - direction;
        }
    }

    public void RotateWeapon()
    {
        if (currentAim != Vector3.zero && direction != Vector3.zero)
        {
            // Get Angle
            CurrentAimAngle = Mathf.Atan2(currentAim.y, currentAim.x) * Mathf.Rad2Deg;
            CurrentAimAngleAbsolute = Mathf.Atan2(currentAimAbsolute.y, currentAimAbsolute.x) * Mathf.Rad2Deg;

            // Clamp our rotation
            if (weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
            {
                CurrentAimAngle = Mathf.Clamp(CurrentAimAngle, -180, 180);
            }
            else
            {
                CurrentAimAngle = Mathf.Clamp(CurrentAimAngle, -180, 180);
            }
            
            lookRotation = Quaternion.Euler(CurrentAimAngle * Vector3.forward);
            transform.rotation = lookRotation;
        }
        else
        {
            CurrentAimAngle = 0f;
            transform.rotation = initialRotation;
        }
    }

    private void EnemyAim()
    {
        currentAimAbsolute = currentAim;
        currentAim = weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight ? currentAim : -currentAim;
        direction = currentAim - transform.position;
    }

    public void SetAim(Vector2 newAim)
    {
        currentAim = newAim;
    }
    
    /// <summary>
    /// Moves our reticle towards our Mouse Position
    /// </summary>
    private void MoveReticle()
    {
        reticle.transform.rotation = Quaternion.identity;
        reticle.transform.position = reticlePosition;
    }

    public void DestroyReticle()
    {
        Destroy(reticle.gameObject);
    }
}
