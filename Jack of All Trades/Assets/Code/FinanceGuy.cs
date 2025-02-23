using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FinanceGuy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 2f;
    private Vector2 movement;


    private int score = 0; // Track the number of files collected
    public Text scoreText; // Reference to the Score UI Text


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity
        rb.freezeRotation = true; // Prevent unwanted rotation
        UpdateScoreUI(); // Initialize score display
    }


    void Update()
    {
        // Get movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        movement = new Vector2(moveX, moveY).normalized; // Normalize to prevent diagonal speed boost
    }


    void FixedUpdate()
    {
        // Apply velocity for movement
        rb.velocity = movement * moveSpeed;
    }


    public void AddScore(int points)
    {
        score += points;  // Increase the score
        UpdateScoreUI(); // Update the UI
    }


    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }


    // Makes objects with the tag "files" disappear on contact
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("files"))
        {
            Destroy(other.gameObject);
        }
    }
}
