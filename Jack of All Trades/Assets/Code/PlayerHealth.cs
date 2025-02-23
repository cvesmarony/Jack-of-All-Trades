using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private Vector3 respawnPoint;
    private bool isDead = false;


    public Text healthText; // Reference to the UI Text component


    void Start()
    {
        currentHealth = maxHealth;
        respawnPoint = transform.position; // Set initial spawn point
        UpdateHealthUI(); // Initialize health display
    }


    public void TakeDamage(int damage)
    {
        if (isDead) return; // Prevent taking damage after death


        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);


        UpdateHealthUI(); // Update UI when health changes


        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        if (isDead) return; // Prevent multiple death calls
        isDead = true;
        Debug.Log("Player has died!");
        StartCoroutine(Respawn());
    }


    IEnumerator Respawn()
    {
        // Hide the player but don't disable the script
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3f); // Wait 3 seconds


        // Restore the player
        transform.position = respawnPoint; // Move to spawn point
        currentHealth = maxHealth; // Reset health
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;


        isDead = false;
        UpdateHealthUI(); // Update UI after respawning
        Debug.Log("Player Respawned!");
    }


    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }
}
