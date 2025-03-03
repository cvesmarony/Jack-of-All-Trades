using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for Light2D
using System.Collections;

public class SecurityGuardWithFlashlight : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float changeDirectionTime = 1f;
    public float detectionRadius = 5f;
    public LayerMask detectionLayer;
    
    public Light2D flashlight;
    public float flashlightOffsetDistance = 0.8f;
    public float flashlightOffsetAngle = -90f;
    
    public int damageAmount = 2; // Damage applied when player is detected
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float timer;
    private Transform target;

    private bool canTakeDamage = true; // Cooldown flag
    private float damageCooldownTime = 5f; // 5-second cooldown

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseNewDirection();
        timer = changeDirectionTime;
    }

    void Update()
    {
        DetectMovement();

        if (target == null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ChooseNewDirection();
                timer = changeDirectionTime;
            }
        }

        UpdateFlashlight();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
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
            target = detected.transform;
        }
        else
        {
            target = null;
        }
    }

    void UpdateFlashlight()
    {
        if (flashlight != null)
        {
            Vector2 movementDirection = rb.velocity.sqrMagnitude > 0.01f ? rb.velocity.normalized : moveDirection;
            Vector2 targetPosition = (Vector2)transform.position + movementDirection * flashlightOffsetDistance;
            flashlight.transform.position = targetPosition;

            float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg + flashlightOffsetAngle;
            flashlight.transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
    }

    // Detect when player enters the flashlight area
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canTakeDamage)
        {
            Debug.Log("Player detected in flashlight! Applying damage.");
            GameHandler.instance.playerGetHit(damageAmount);
            StartCoroutine(DamageCooldown());
        }
    }

    // Cooldown to prevent continuous damage
    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false; // Disable damage temporarily
        yield return new WaitForSeconds(damageCooldownTime); // Wait for cooldown
        canTakeDamage = true; // Enable damage again
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
