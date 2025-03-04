using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightDetection : MonoBehaviour
{
    public Light2D flashlight;
    public CapsuleCollider2D detectionCollider;
    public int damagePerHit = 1;
    public float damageInterval = 0.3f;
    public LayerMask detectionLayer; // Player layer
    public LayerMask cubicleLayer; // Obstacle layer

    private float damageTimer = 0f;
    private bool playerInLight = false;

    void FixedUpdate()
    {
        if (flashlight != null && detectionCollider != null)
        {
            detectionCollider.transform.position = flashlight.transform.position;
            detectionCollider.transform.rotation = flashlight.transform.rotation;
        }

        if (playerInLight && GameHandler.instance != null)
        {
            damageTimer += Time.fixedDeltaTime;
            if (damageTimer >= damageInterval)
            {
                GameHandler.instance.playerGetHit(damagePerHit);
                Debug.Log("⚡ Player takes damage!");
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & detectionLayer) != 0)
        {
            Vector2 directionToPlayer = (other.transform.position - flashlight.transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(flashlight.transform.position, other.transform.position);

            Debug.DrawRay(flashlight.transform.position, directionToPlayer * distanceToPlayer, Color.yellow, 0.1f);
            Debug.Log("🔍 Checking visibility for Player...");

            RaycastHit2D hit = Physics2D.Raycast(flashlight.transform.position, directionToPlayer, distanceToPlayer, detectionLayer | cubicleLayer);

            if (hit.collider != null)
            {
                Debug.Log("🔵 Raycast hit: " + hit.collider.gameObject.name);

                if (((1 << hit.collider.gameObject.layer) & detectionLayer) != 0) // Hit Player
                {
                    Debug.Log("✅ Player is visible!");
                    playerInLight = true;
                }
                else if (((1 << hit.collider.gameObject.layer) & cubicleLayer) != 0) // Hit an Obstacle
                {
                    Debug.Log("⛔ Obstacle detected: " + hit.collider.gameObject.name);
                    playerInLight = false;
                }
            }
            else
            {
                Debug.Log("❌ No hit detected, something is wrong with layers.");
                playerInLight = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & detectionLayer) != 0)
        {
            Debug.Log("🚪 Player left the flashlight zone.");
            playerInLight = false;
            damageTimer = 0f;
        }
    }
}
