using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public Pause pauseScript;
    void Update()
    {
        
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Death") {
                if (Input.GetKeyUp(KeyCode.R)) {
                    SceneManager.LoadScene(LevelSwitcher.levelNames[LevelSwitcher.levelNum]);
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene("Menu");
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                QuitGame();
            }
        }
        else if (pauseScript.isPaused) {

            if (Input.GetKeyUp(KeyCode.R)) {
                StartGame();

            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene("Menu");
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                QuitGame();
            }
        

        }

        

        }

    public void StartGame() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1f;
        GameObject hud = GameObject.Find("HUD");
        hud.SetActive(true);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

}
