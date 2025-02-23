using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightDetection : MonoBehaviour
{
    public Light2D flashlight; // Assign in Inspector
    public CapsuleCollider2D detectionCollider; // Assign in Inspector
    public int damagePerSecond = 10;
    public float damageInterval = 0.2f; // Damage every 0.2 seconds

    private float damageTimer = 0f;
    private bool playerInLight = false;

    void Update()
    {
        // Sync collider position and rotation with the flashlight
        if (flashlight != null && detectionCollider != null)
        {
            detectionCollider.transform.position = flashlight.transform.position;
            detectionCollider.transform.rotation = flashlight.transform.rotation;
        }

        // Apply damage at set intervals
        if (playerInLight)
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
}
