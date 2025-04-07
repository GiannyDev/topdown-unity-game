using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// Controls the current movement of this character
    /// </summary>
    public Vector2 CurrentMovement { get; set; }
    
    /// <summary>
    /// Returns if this character can move normally (When dashing we can't)
    /// </summary>
    public bool NormalMovement { get; set; }
    
    // Internal
    private Rigidbody2D myRigidbody2D;
    private Vector2 recoilMovement;
    
    private void Start()
    {
        NormalMovement = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        Recoil();
        
        if (NormalMovement)
        {
            MoveCharacter();
        }
    }
    
    /// <summary>
    /// Moves our Character
    /// </summary>
    private void MoveCharacter()
    {
        Vector2 currentMovePosition = myRigidbody2D.position + CurrentMovement * Time.fixedDeltaTime;
        myRigidbody2D.MovePosition(currentMovePosition);
    }

    public void ApplyRecoil(Vector2 recoilDirection, float recoilForce)
    {
        recoilMovement = recoilDirection.normalized * recoilForce;
    }
    
    /// <summary>
    /// Extra Move in case we need it
    /// </summary>
    /// <param name="newPosition">Moves this character to this position</param>
    public void MovePosition(Vector2 newPosition)
    {
        myRigidbody2D.MovePosition(newPosition);
    }
    
    /// <summary>
    /// Sets the current movement of our character
    /// </summary>
    /// <param name="newPosition">The new Position we need to be</param>
    public void SetMovement(Vector2 newPosition)
    {
        CurrentMovement = newPosition;
    }

    private void Recoil()
    {
        if (recoilMovement.magnitude > 0.1f)
        {
            myRigidbody2D.AddForce(recoilMovement);
        }
    }
}