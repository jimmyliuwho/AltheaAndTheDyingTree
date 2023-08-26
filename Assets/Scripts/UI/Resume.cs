using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour
{

    private CanvasGroup canvasGroup;

    void Awake () {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            Debug.LogError("Component not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
            Unpause();
        
    }

    public void Unpause()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f;
    }
}
