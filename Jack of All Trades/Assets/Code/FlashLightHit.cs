using UnityEngine;

public class FlashLightHit : MonoBehaviour {
    public int damage = 2; // Amount of damage dealt by flashlight

    // When the player comes in contact with the flashlight
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // Deduct health from the player
            GameHandler gameHandler = FindObjectOfType<GameHandler>();
            gameHandler.playerGetHit(damage);
        }
    }
}
