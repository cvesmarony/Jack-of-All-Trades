using UnityEngine;
using UnityEngine.UI; // For UI Slider
using UnityEngine.SceneManagement;

public class NPCInteraction : MonoBehaviour
{
    //public int score = GameHandler.gotData; 
    public int requiredScore = 2; // Score needed to interact
    public Slider progressSlider; // UI Slider to show progress
    public float interactionTime = 3f; // Time needed for full interaction

    private float currentInteractionTime = 0f;
    private bool isInteracting = false;
    private bool playerInRange = false; // Check if player is near

    public delegate void LevelComplete(); 
    public static event LevelComplete OnLevelComplete; // Event for level completion

    public static int currentLevelIndex = 0;

    void Start()
    {
        OnLevelComplete += LoadNextLevel;
    }


    void Update()
    {
        if(playerInRange) {
            Debug.Log("Player in range. the current data is: " + GameHandler.gotData);
        }
        // Only interact if the player is in range and score meets the requirement
        if (playerInRange && GameHandler.gotData >= requiredScore && Input.GetKey(KeyCode.Space))
        {
            isInteracting = true;
            currentInteractionTime += Time.deltaTime;
            progressSlider.value = currentInteractionTime / interactionTime;

            // If interaction completes
            if (currentInteractionTime >= interactionTime)
            {
                GameHandler.gotData = 0; // Set score to 0
                Debug.Log("Interaction Complete!");

                // Signal end of level
                if (OnLevelComplete != null)
                {
                    OnLevelComplete();
                }
            }
        }
        else
        {
            // Reset interaction if Space Bar is released
            if (isInteracting)
            {
                isInteracting = false;
                currentInteractionTime = 0f;
                progressSlider.value = 0;
            }
        }
    }

    // Detect player entering interaction zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure Player has "Player" tag
        {
            playerInRange = true;
        }
    }

    // Detect player leaving interaction zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            isInteracting = false;
            currentInteractionTime = 0f;
            progressSlider.value = 0; // Reset progress when leaving
        }
    }

    void LoadNextLevel()
    {
        Debug.Log("Current Level Index: " + currentLevelIndex);
        currentLevelIndex++;
        
        if(currentLevelIndex > 4) {
            SceneManager.LoadScene("EndWin");
            return;
        } 
        else 
        {
            Debug.Log("Next Level Index: " + currentLevelIndex);
            SceneManager.LoadScene("LEVEL_" + currentLevelIndex); 
        }
    }

    void OnDestroy()
    {
        OnLevelComplete -= LoadNextLevel; 
    }
}
