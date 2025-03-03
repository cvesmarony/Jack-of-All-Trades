using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float detectionRadius = 5f;
    public LayerMask detectionLayer;
    public LayerMask cubicleLayer;

    public Light2D flashlight;
    public float flashlightOffsetDistance = 0.8f;
    public float flashlightOffsetAngle = -90f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Transform target;
    private Vector2 lastKnownPlayerPosition;
    private bool inRandomMode = true;
    
    public float randomChangeInterval = 1.5f; // Time before changing direction
    private float randomMoveTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseRandomDirection();
        randomMoveTimer = randomChangeInterval;
    }

    void Update()
    {
        DetectPlayer();
        UpdateFlashlight();
    }

    void FixedUpdate()
    {
        if (!inRandomMode && target != null)
        {
            Vector2 directionToPlayer = ((Vector2)target.position - (Vector2)transform.position).normalized;
            float distanceToPlayer = Vector2.Distance((Vector2)transform.position, (Vector2)target.position);

            if (!Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, cubicleLayer))
            {
                rb.velocity = directionToPlayer * moveSpeed;
            }
            else
            {
                inRandomMode = true;
                ChooseRandomDirection();
            }
        }
        else
        {
            // Random movement timer
            randomMoveTimer -= Time.deltaTime;
            if (randomMoveTimer <= 0f)
            {
                ChooseRandomDirection();
                randomMoveTimer = randomChangeInterval; // Reset timer
            }

            rb.velocity = moveDirection * moveSpeed;
        }
    }

    void DetectPlayer()
    {
        Collider2D detected = Physics2D.OverlapCircle(transform.position, detectionRadius, detectionLayer);
        if (detected != null)
        {
            Vector2 directionToPlayer = ((Vector2)detected.transform.position - (Vector2)transform.position).normalized;
            float distanceToPlayer = Vector2.Distance((Vector2)transform.position, (Vector2)detected.transform.position);

            if (!Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, cubicleLayer))
            {
                target = detected.transform;
                inRandomMode = false;
            }
            else
            {
                target = null;
            }
        }
        else
        {
            target = null;
        }
    }

    void ChooseRandomDirection()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        rb.velocity = moveDirection * moveSpeed;
    }

    void UpdateFlashlight()
    {
        if (flashlight != null)
        {
            Vector2 movementDirection = rb.velocity.sqrMagnitude > 0.01f ? rb.velocity.normalized : moveDirection;
            flashlight.transform.position = (Vector2)transform.position + movementDirection * flashlightOffsetDistance;

            float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg + flashlightOffsetAngle;
            flashlight.transform.rotation = Quaternion.Lerp(
                flashlight.transform.rotation,
                Quaternion.Euler(0, 0, targetAngle),
                Time.deltaTime * 10f
            );
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & cubicleLayer) != 0)
        {
            inRandomMode = true;
            ChooseRandomDirection();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
