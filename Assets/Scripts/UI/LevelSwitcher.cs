using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{

    public static int levelNum;
    public static int globalPineCount;

    public static string[] levelNames = new string[] {
            "Intro",
            "Level_0",
            "Level_1",
            "Level_2",
            "Level_3",
            "Level_4",
            "Level_5",
            "Level_7",
            "Audit",
    };

    // Start is called before the first frame update
    void Start()
    {
        levelNum = 0;
    }

    public void NextLevel()
    {
        levelNum++;
        Debug.Log("Level: "+levelNum);
        SceneManager.LoadScene(levelNames[levelNum]);
        Time.timeScale = 1f;
    }

}
