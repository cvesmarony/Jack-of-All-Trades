using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for Light2D

public class SecurityGuardWithFlashlight : MonoBehaviour
{
    public float moveSpeed = 1.5f; // Speed of movement
    public float changeDirectionTime = 1f; // Time interval before changing direction
    public float detectionRadius = 5f; // Radius to detect movement
    public LayerMask detectionLayer; // Layer to detect player

    public Light2D flashlight; // Reference to the flashlight
    public float flashlightOffsetDistance = 0.8f; // How far in front of the guard the light should be
    public float flashlightOffsetAngle = -90f; // Adjust if flashlight is misaligned

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float timer;
    private Transform target; // Player target if detected

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseNewDirection();
        timer = changeDirectionTime;
    }

    void Update()
    {
        DetectMovement();

        if (target == null) // No player detected, continue random movement
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ChooseNewDirection();
                timer = changeDirectionTime;
            }
        }

        UpdateFlashlight(); // Make sure flashlight follows movement direction
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // Move towards detected player
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            // Move in a random direction
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    void ChooseNewDirection()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    void DetectMovement()
    {
        Collider2D detected = Physics2D.OverlapCircle(transform.position, detectionRadius, detectionLayer);
        if (detected != null)
        {
            target = detected.transform; // Lock onto detected object
        }
        else
        {
            target = null; // Resume random movement
        }
    }

    void UpdateFlashlight()
    {
        if (flashlight != null)
        {
            // Determine movement direction
            Vector2 movementDirection = rb.velocity.sqrMagnitude > 0.01f ? rb.velocity.normalized : moveDirection;

            // Set flashlight position in front of guard
            Vector2 targetPosition = (Vector2)transform.position + movementDirection * flashlightOffsetDistance;
            flashlight.transform.position = targetPosition;

            // Rotate flashlight to match movement direction
            float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg + flashlightOffsetAngle;
            flashlight.transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw detection radius in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
