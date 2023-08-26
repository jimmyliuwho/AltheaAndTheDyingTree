using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeystrokeListener : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Menu") {
                LevelSwitcher.levelNum = 0;
                LevelSwitcher.globalPineCount = 0;
                Debug.Log("Pinecone Count Reset");
                SceneManager.LoadScene(LevelSwitcher.levelNames[0]);
                Time.timeScale = 1f;
            } else if (currentScene.name == "Intro") {
                LevelSwitcher.levelNum += 1;
                SceneManager.LoadScene(LevelSwitcher.levelNames[LevelSwitcher.levelNum]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Menu") {
                SceneManager.LoadScene("Credits");
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            QuitGame();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Credits" || currentScene.name == "LevelComplete" || currentScene.name == "WinScene") {
                SceneManager.LoadScene("Menu");
            }
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "LevelComplete") {
                LevelSwitcher.levelNum += 1;
                SceneManager.LoadScene(LevelSwitcher.levelNames[LevelSwitcher.levelNum]);
            }
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Menu")
            {
                GameObject.Find("Controls").GetComponent<GameStarter>().ToggleControls();
            }
        }
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
