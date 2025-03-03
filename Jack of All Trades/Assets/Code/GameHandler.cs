using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

    private GameObject player;
    private GameObject healthText;
    
    public static int playerHealth = 20; // Starting health
    public static int gotData = 0; // Data collected by the player
    public GameObject DataText; // Display for data collection
    public GameObject HealthText; // Display for health
    public static bool stairCaseUnlocked = false;

    private string sceneName;
    public static string lastLevelDied; // Allows replaying the Level where the player died

    void Start() {
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;

        // Display initial stats
        updateStatsDisplay();
    }

    // Update health and data display
    public void updateStatsDisplay() {
        Text tokensTextTemp = DataText.GetComponent<Text>();
        tokensTextTemp.text = "Data: " + gotData;

        Text healthTextTemp = HealthText.GetComponent<Text>();
        healthTextTemp.text = "Health: " + playerHealth;
    }

    // Deduct health when player is hit by security guard flashlight
    public void playerGetHit(int damage) {
        playerHealth -= damage;
        if (playerHealth <= 0) {
            playerHealth = 0;
            updateStatsDisplay();
            gameOver();
        } else {
            updateStatsDisplay();
        }
    }

    // Player interaction with NPC (data transfer)
    public void playerGetTokens(int newTokens) {
        gotData += newTokens;
        updateStatsDisplay();
    }

    // Transition to the next level when the player interacts with the NPC
    public void CompleteLevel() {
        Debug.Log("Level Completed!");

        // Reset score (data) after completing the level
        gotData = 0;

        // Load the next scene
        /*
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex); // Load next level
        } else {
            SceneManager.LoadScene("EndWin"); // If it's the last level, go to EndWin
        }
        */
    }

    // Game Over logic
    public void gameOver() {
        SceneManager.LoadScene("GameOver");
    }

    // Starting the game
    public void StartGame() {
        SceneManager.LoadScene("TUTORIAL_1"); // Start from Level 1
    }

    // Restart the game (if player wants to restart)
    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        playerHealth = 20; // Reset player health to default
        gotData = 0; // Reset data to default
        updateStatsDisplay();
    }

    // Quit game
    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Access the last level where the player died
    public void ReplayLastLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(lastLevelDied);
        updateStatsDisplay();
    }

    // Transition to Credits scene
    public void Credits() {
        SceneManager.LoadScene("CreditsMenu");
    }
}
