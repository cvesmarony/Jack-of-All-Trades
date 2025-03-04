 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameHandler_PauseMenu : MonoBehaviour {

    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;

    void Awake(){
        pauseMenuUI.SetActive(true); // so slider can be set
    }

    void Start(){
        pauseMenuUI.SetActive(false);
        GameisPaused = false;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameisPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Pause(){
    Debug.Log("Pausing game...");
    
    Time.timeScale = 0f;
    GameisPaused = true;

    // Load the pause menu scene
    SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive); // Additively loads the pause menu
}


    public void Resume(){
    Debug.Log("Resume function called. Game is now resuming.");
    
    Time.timeScale = 1f;
    GameisPaused = false;

    // Unload the pause menu scene
    SceneManager.UnloadSceneAsync("PauseMenu"); 

    Debug.Log("Pause menu scene unloaded.");
}


}