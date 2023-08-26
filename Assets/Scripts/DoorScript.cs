using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTIme = 2f;
    private void OnTriggerEnter(Collider other)
    {
        // SceneManager.LoadScene("LevelComplete");
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level_7")
        {
            LevelSwitcher.globalPineCount += GameObject.Find("PLAYER FINAL").GetComponentInChildren<PlayerController>().pineconeCount;
            //LevelSwitcher.globalPineCount = 40;
            SceneManager.LoadScene("FinalLevel");
            LevelSwitcher.levelNum++;
        } else if (currentScene.name == "FinalLevel")
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            StartCoroutine(LoadLevel());
        }
       
    }

    IEnumerator LoadLevel() 
    {
        // play animation for transition
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTIme);
        //Load Scene
        LevelSwitcher.globalPineCount += GameObject.Find("PLAYER FINAL").GetComponentInChildren<PlayerController>().pineconeCount;
        SceneManager.LoadScene("LevelComplete");


    }
}

