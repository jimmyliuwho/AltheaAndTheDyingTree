using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTextBox : MonoBehaviour
{
    [SerializeField] public int dataNum;
    private bool done;
    private TutorialTextUI tutText;

    private void Start()
    {
        done = false;
        tutText = GameObject.Find("Pop-up Text").GetComponent<TutorialTextUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "PLAYER FINAL")
        {
            if (LevelSwitcher.levelNum == 8)
            { 
                tutText.Toggle();
                Debug.Log("something");
            }
            else if (!done)
            {
                GameObject textUI = GameObject.Find("Pop-up Text");
                textUI.GetComponent<TutorialTextUI>().CloseTextBox();
                done = true;
            }
        }
    }
}
