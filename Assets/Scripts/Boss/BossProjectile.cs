using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Transform thisTransform;
    
    private float speed;
    private float angle;
    private float acceleration;

    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    private bool canMove;
    
    private void Awake()
    {
        canMove = true;
        thisTransform = transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        MoveProjectile();
    }

    public void Shoot(float newAngle, float newSpeed, float newAcceleration)
    {
        angle = newAngle;
        speed = newSpeed;
        acceleration = newAcceleration;

        // Set new Rotation
        Vector3 projectileAngle = thisTransform.rotation.eulerAngles;
        thisTransform.rotation = Quaternion.Euler(projectileAngle.x, projectileAngle.y, newAngle);
    }

    private void MoveProjectile()
    {
        if (canMove)
        {
            Vector3 projectileAngle = thisTransform.rotation.eulerAngles;
            Quaternion newRotation = thisTransform.rotation;
        
            // Set Rotation
            float angleToAdd = acceleration * Time.deltaTime;
            newRotation = Quaternion.Euler(projectileAngle.x, projectileAngle.y, projectileAngle.z + angleToAdd);
        
            // Apply acceleration
            speed += acceleration * Time.deltaTime;
        
            // Move
            Vector3 newPosition = thisTransform.position + thisTransform.up * (speed * Time.deltaTime);
            thisTransform.SetPositionAndRotation(newPosition, newRotation);
        }
    }

    public void DisableBossProjectile()
    {
        canMove = false;
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
    }

    public void EnableBossProjectile()
    {
        canMove = true;
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
    }
}
