using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightDetection : MonoBehaviour
{
    public Light2D flashlight; // Assign in Inspector
    public CapsuleCollider2D detectionCollider; // Assign in Inspector
    public LayerMask obstacleLayer; // Assign this to "Walls" or obstacles in Inspector
    public int damagePerSecond = 5;
    public float damageInterval = 0.1f; // Damage every 0.1 seconds

    private float damageTimer = 0f;
    private bool playerInLight = false;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        // Sync collider position and rotation with the flashlight
        if (flashlight != null && detectionCollider != null)
        {
            detectionCollider.transform.position = flashlight.transform.position;
            detectionCollider.transform.rotation = flashlight.transform.rotation;
        }

        // Apply damage only if the player is actually visible (not blocked)
        if (playerInLight && PlayerIsActuallyLit())
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    int damageAmount = Mathf.CeilToInt(damagePerSecond * damageInterval); // Convert to int
                    playerHealth.TakeDamage(damageAmount);
                }
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInLight = false;
            damageTimer = 0f; // Reset timer when leaving
        }
    }

    private bool PlayerIsActuallyLit()
    {
        if (playerTransform == null) return false;

        Vector2 directionToPlayer = (playerTransform.position - flashlight.transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(flashlight.transform.position, playerTransform.position);

        // Raycast to check if something is blocking the light
        RaycastHit2D hit = Physics2D.Raycast(flashlight.transform.position, directionToPlayer, distanceToPlayer, obstacleLayer);

        return hit.collider == null; // If no obstacle was hit, the player is visible
    }
}
