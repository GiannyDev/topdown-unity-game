using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterComponents
{
    public enum FlipMode
    {
        MovementDirection,
        WeaponDirection
    }

    [SerializeField] private FlipMode flipMode = FlipMode.MovementDirection;
    [SerializeField] private float threshold = 0.1f;

    /// <summary>
    /// Returns if our character is facing Right
    /// </summary>
    public bool FacingRight { get; set; }
    
    private void Awake()
    {
        FacingRight = true;
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        
        if (flipMode == FlipMode.MovementDirection)
        {
            FlipToMoveDirection();
        }
        else
        {
            FlipToWeaponDirection();
        }
    }

    /// <summary>
    /// Flips our character by the direction we are moving
    /// </summary>
    private void FlipToMoveDirection()
    {
        if (controller.CurrentMovement.normalized.magnitude > threshold)
        {
            if (controller.CurrentMovement.normalized.x > 0)
            {
                FaceDirection(1);
            }
            else
            {
                FaceDirection(-1);
            }
        }
    }

    /// <summary>
    /// Flips our character by our Weapon Aiming
    /// </summary>
    private void FlipToWeaponDirection()
    {
        if (characterWeapon != null)
        {
            float weaponAngle = characterWeapon.WeaponAim.CurrentAimAngleAbsolute;
            if (weaponAngle > 90 || weaponAngle < -90)
            {
                FaceDirection(-1);
            }
            else
            {
                FaceDirection(1);
            }
        }
    }

    /// <summary>
    /// Makes our character face the direction in which is moving
    /// </summary>
    /// <param name="newDirection">Direction to face</param>
    private void FaceDirection(int newDirection)
    {
        if (newDirection == 1)
        { 
            character.CharacterSprite.transform.localScale = new Vector3(1,1,1);
            FacingRight = true;
        }
        else
        {
            character.CharacterSprite.transform.localScale = new Vector3(-1,1,1);
            FacingRight = false;
        }
    }
}