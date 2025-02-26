using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

      private GameObject player;
      private GameObject health;
    //   public static int playerHealth = 100;
    //   public int StartPlayerHealth = 100;
    //   public GameObject healthText;

      public static int gotData = 0;
      public GameObject DataText;

      public bool isDefending = false;

      public static bool stairCaseUnlocked = false;
      //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

      private string sceneName;
      public static string lastLevelDied;  //allows replaying the Level where you died

      void Start(){
            player = GameObject.FindWithTag("Player");
            sceneName = SceneManager.GetActiveScene().name;
            //if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
                //   playerHealth = StartPlayerHealth;
            //}
            updateStatsDisplay();
      }

      public void playerGetTokens(int newTokens){
            gotData += newTokens;
            updateStatsDisplay();
      }

      /*public void playerGetHit(int damage){
           if (isDefending == false){
                  playerHealth -= damage;
                  if (playerHealth >=0){
                        updateStatsDisplay();
                  }
                  if (damage > 0){
                        //play GetHit animation:
                        //player.GetComponent<PlayerHurt>().playerHit();
                  }
            }

           if (playerHealth > StartPlayerHealth){
                  playerHealth = StartPlayerHealth;
                  updateStatsDisplay();
            }

           if (playerHealth <= 0){
                  playerHealth = 0;
                  updateStatsDisplay();
                  playerDies();
            }
      }
*/
      public void updateStatsDisplay(){
            // Text healthTextTemp = healthText.GetComponent<Text>();
            // healthTextTemp.text = "HEALTH: " + playerHealth;

            Text tokensTextTemp = DataText.GetComponent<Text>();
            tokensTextTemp.text = "Data: " + gotData;
      }

      public void gameOver() {
            SceneManager.LoadScene("GameOver");
      }

      public void endWin() {
            if (gotData >= 4) {
                  endWin();
                  return;
            }
           SceneManager.LoadScene("EndWin");
      }



     /* public void playerDies(){
            //player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
            lastLevelDied = sceneName;       //allows replaying the Level where you died
            StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
            //player.GetComponent<PlayerMove>().isAlive = false;
            //player.GetComponent<PlayerJump>().isAlive = false;
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("EndLose");
      }
*/
      public void StartGame() {
            SceneManager.LoadScene("Office");
      }

      // Return to MainMenu
      public void RestartGame() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
             // Reset all static variables here, for new games:
            GetComponent<PlayerHealth>().restart();
      }

      // Replay the Level where you died
      public void ReplayLastLevel() {
            Time.timeScale = 1f;
            SceneManager.LoadScene(lastLevelDied);
             // Reset all static variables here, for new games:
            // playerHealth = StartPlayerHealth;
            GetComponent<PlayerHealth>().restart();
      }

      public void QuitGame() {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

      public void Credits() {
            SceneManager.LoadScene("CreditsMenu");
      }
}