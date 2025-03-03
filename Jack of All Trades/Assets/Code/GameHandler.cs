using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance; // Singleton pattern for easy access

    public static int playerHealth = 20; // Starting health
    public static int gotData = 0; // Data collected by the player
    public GameObject DataText; // Display for data collection
    public GameObject HealthText; // Display for health UI

    public static bool stairCaseUnlocked = false;
    public static string lastLevelDied; // Stores the last level where the player died

    private bool isGameOver = false; // Prevents multiple game over calls

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (HealthText == null)
        {
            Debug.LogError("HealthText UI not assigned in the Inspector!");
        }

        if (DataText == null)
        {
            Debug.LogError("DataText UI not assigned in the Inspector!");
        }

        updateStatsDisplay();
    }

    // Update health and data display
    public void updateStatsDisplay()
    {
        if (DataText != null)
        {
            Text tokensTextTemp = DataText.GetComponent<Text>();
            tokensTextTemp.text = "Data: " + gotData;
        }

        if (HealthText != null)
        {
            Text healthTextTemp = HealthText.GetComponent<Text>();
            healthTextTemp.text = "Health: " + playerHealth;
        }
    }

    // Deduct health when player is hit by security guard flashlight
    public void playerGetHit(int damage)
    {
        if (isGameOver) return; // Prevent health from going below 0 after game over

        playerHealth -= damage;
        Debug.Log($"Player took {damage} damage. New Health: {playerHealth}");

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            isGameOver = true;
            updateStatsDisplay();
            gameOver();
        }
        else
        {
            updateStatsDisplay();
        }
    }

    // Player interaction with NPC (data transfer)
    public void playerGetTokens(int newTokens)
    {
        gotData += newTokens;
        updateStatsDisplay();
    }

    // Handles Game Over
    public void gameOver()
    {
        if (isGameOver) return; // Prevent multiple calls
        Debug.Log("Game Over! Restarting...");
        SceneManager.LoadScene("GameOver");
    }

    // Restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        playerHealth = 20;
        gotData = 0;
        isGameOver = false;
    }

    // Quit game
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Replay the last level
    public void ReplayLastLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(lastLevelDied);
    }

    // Transition to Credits
    public void Credits()
    {
        SceneManager.LoadScene("CreditsMenu");
    }
}
