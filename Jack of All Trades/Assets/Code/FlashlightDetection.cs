using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightDetection : MonoBehaviour
{
    public Light2D flashlight; // Assign in Inspector
    public CapsuleCollider2D detectionCollider; // Assign in Inspector
    public int damagePerHit = 1; // Damage per hit
    public float damageInterval = 0.3f; // Time between damage ticks

    private float damageTimer = 0f;
    private bool playerInLight = false;

    void FixedUpdate()
    {
        // Sync collider position and rotation with the flashlight
        if (flashlight != null && detectionCollider != null)
        {
            detectionCollider.transform.position = flashlight.transform.position;
            detectionCollider.transform.rotation = flashlight.transform.rotation;
        }

        // Damage the player at a steady rate
        if (playerInLight && GameHandler.instance != null)
        {
            damageTimer += Time.fixedDeltaTime;
            if (damageTimer >= damageInterval) // Apply damage every 0.5s
            {
                GameHandler.instance.playerGetHit(damagePerHit);
                damageTimer = 0f; // Reset timer
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
