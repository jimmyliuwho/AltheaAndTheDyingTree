using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private GameObject textUI;
    GameObject hud;
    public bool isPaused;

    void Awake () {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            Debug.LogError("Component not found");
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        hud = GameObject.Find("HUD");
        textUI = GameObject.Find("Pop-up Text");
        isPaused = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (canvasGroup.interactable)
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                Time.timeScale = 1f;
                hud.SetActive(true);
                isPaused = false;
            }
            else
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
                hud.SetActive(false);
                textUI.GetComponent<TutorialTextUI>().CloseTextBox();
                isPaused = true;
            }
        }

        if (isPaused && Input.GetKeyUp(KeyCode.M)){
            NavigateMenu();
        }
        
    }
    public void NavigateMenu()
    {
        LevelSwitcher.levelNum = 0;
        SceneManager.LoadScene("Menu");
    }
}
