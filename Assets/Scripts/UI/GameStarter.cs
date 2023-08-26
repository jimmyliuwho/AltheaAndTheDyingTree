using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    public static bool invertedControls;
    private GameObject cb;

    // Start is called before the first frame update
    void Start()
    {
        invertedControls = false;
        cb = GameObject.Find("ControlsButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        StartAtLevel(LevelSwitcher.levelNum);
        Time.timeScale = 1f;
    }

    public void StartGame() {
        LevelSwitcher.levelNum = 0;
        StartAtLevel(LevelSwitcher.levelNum);
        Time.timeScale = 1f;
    }

    public void StartAtLevel(int i)
    {
        LevelSwitcher.levelNum = i;
        SceneManager.LoadScene(LevelSwitcher.levelNames[i]);
        Time.timeScale = 1f;
    }

    public void ToggleControls()
    {
        invertedControls = !invertedControls;
        if (invertedControls)
        {
            cb.GetComponent<Text>().text = "Q for Controls: Inverted";
        }
        else
        {
            cb.GetComponent<Text>().text = "Q for Controls: Regular";
        }
    }


}
