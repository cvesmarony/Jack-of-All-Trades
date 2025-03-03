using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance; // Singleton instance

    private GameObject player;

    public static int maxHealth = 20;
    public static int currHealth;
    public static int gotData = 0; // Data collected by the player
    public GameObject DataText; // Display for data collection
    public GameObject HealthText; // Display for health UI
    public static bool stairCaseUnlocked = false;

    void Start()
    {
        if (instance == null)
        {
            instance = this; // Assign instance for singleton
        }

        player = GameObject.FindWithTag("Player");

        if (HealthText == null)
        {
            Debug.LogError("HealthText UI not assigned in the Inspector!");
        }

        if (DataText == null)
        {
            Debug.LogError("DataText UI not assigned in the Inspector!");
        }

        currHealth = maxHealth;
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
            healthTextTemp.text = "Health: " + currHealth;
        }
    }

    public void playerGetHit(int damage)
    {
        currHealth -= damage;
        Debug.Log("Player took damage: " + damage + ", New Health: " + currHealth);

        if (currHealth <= 0)
        {
            currHealth = 0;
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
