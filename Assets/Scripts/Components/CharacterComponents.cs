using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponents : MonoBehaviour
{
    protected float horizontalInput;
    protected float verticalInput;

    protected CharacterController controller;
    protected CharacterMovement characterMovement;
    protected CharacterWeapon characterWeapon;
    protected Animator animator;
    protected Character character;

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        character = GetComponent<Character>();
        characterWeapon = GetComponent<CharacterWeapon>();
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        HandleAbility();
    }

    /// <summary>
    /// Main method. Here we put the logic of each ability
    /// </summary>
    protected virtual void HandleAbility()
    {
        InternalInput();
        HandleInput();
    }

    /// <summary>
    /// Here we get the necessary input we need to perform our actions
    /// </summary>
    protected virtual void HandleInput() { }

    /// <summary>
    /// Here get the main input we need to control our character
    /// </summary>
    protected virtual void InternalInput()
    {
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
    }
}