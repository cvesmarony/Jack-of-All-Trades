using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler_PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;
    private string pauseSceneName = "PauseMenu"; // Make sure this matches the actual scene name

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Debug.Log("Pausing game...");

        Time.timeScale = 0f;
        GameisPaused = true;

        // Load the pause menu scene additively
        SceneManager.LoadScene(pauseSceneName, LoadSceneMode.Additive);
    }

    public void Resume()
    {
        Debug.Log("Resuming game...");

        Time.timeScale = 1f;
        GameisPaused = false;

        // Unload the pause menu scene
        StartCoroutine(UnloadPauseScene());
    }

    private IEnumerator UnloadPauseScene()
    {
        yield return null; // Wait one frame to avoid conflicts

        if (SceneManager.GetSceneByName(pauseSceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(pauseSceneName);
            Debug.Log("Pause menu scene unloaded.");
        }
        else
        {
            Debug.LogWarning("Pause menu scene was not loaded.");
        }
    }
}
