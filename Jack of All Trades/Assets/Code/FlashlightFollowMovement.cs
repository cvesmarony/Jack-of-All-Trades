using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class FlashlightFollowMovement : MonoBehaviour
{
    public Light2D flashlight; 
    public float offsetAngle = 0f; // Adjust if needed

    private Vector2 lastDirection = Vector2.right; // Default direction
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (flashlight != null)
        {
            // Move the flashlight with the player
            flashlight.transform.position = transform.position;

            // Get movement direction from Rigidbody2D
            Vector2 moveDirection = rb.velocity;

            if (moveDirection.magnitude > 0.1f) // If moving, update direction
            {
                lastDirection = moveDirection.normalized;
            }

            float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;

            // Apply rotation
            flashlight.transform.rotation = Quaternion.Euler(0, 0, angle + offsetAngle);
        }
    }
}
