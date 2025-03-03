using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance; // Singleton instance

    private GameObject player;
    
    public static int playerHealth = 20; // Starting health
    public static int gotData = 0; // Data collected by the player
    public GameObject DataText; // Display for data collection
    public GameObject HealthText; // Display for health UI
    public static bool stairCaseUnlocked = false;

    private string sceneName;
    public static string lastLevelDied;

    void Start()
    {
        if (instance == null)
        {
            instance = this; // Assign instance for singleton
        }

        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;

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

    public void playerGetHit(int damage)
    {
        playerHealth -= damage;
        Debug.Log("Player took damage: " + damage + ", New Health: " + playerHealth);

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            updateStatsDisplay();
            gameOver();
        }
        else
        {
            updateStatsDisplay();
        }
    }

    public void gameOver()
    {
        Debug.Log("Game Over! Restarting...");
        SceneManager.LoadScene("GameOver");
    }
}
