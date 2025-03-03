 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

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
        if (!GameisPaused){
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameisPaused = true;}
        else{ Resume ();}
            //NOTE: This added conditional is for a pause button
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }
}