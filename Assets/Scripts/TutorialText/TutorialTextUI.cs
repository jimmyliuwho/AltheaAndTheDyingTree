using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TutorialTextUI : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TutorialTextObject[] triggeredText;

    private bool stillTyping;

    public static int[] tutStartNum = new int[] {0, 0, 1, 2, 3, 4, 5, 6, 7};

    private TypewriterEffect typeEffect;
    void Start()
    {
        typeEffect = GetComponent<TypewriterEffect>();
        CloseTextBox();
        Debug.Log("Switching to Level: "+LevelSwitcher.levelNum);
        if (LevelSwitcher.levelNum < 4)
        {
            ShowDialogue(tutStartNum[LevelSwitcher.levelNum]);
        }
    }

    public void ShowDialogue(int i)
    {
        stillTyping = true;
        if (!textBox.activeSelf)
        {
            TutorialTextObject tutorialTextObject = triggeredText[i];
            textBox.SetActive(true);
            StartCoroutine(StepThroughText(tutorialTextObject));
        }
    }

    private IEnumerator StepThroughText(TutorialTextObject tutorialTextObject)
    {
        foreach (string tutorialText in tutorialTextObject.TutorialText)
        {
            yield return typeEffect.Run(tutorialText, textLabel);
            stillTyping = false;
            //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.C));
        }
        //CloseTextBox();
    }

    public void CloseTextBox()
    {
        textBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    public void Toggle()
    {
        if (textBox.activeSelf && !stillTyping)
        {
            CloseTextBox();
        } else
        {
            if (LevelSwitcher.levelNum < tutStartNum.Length)
            {
                ShowDialogue(tutStartNum[LevelSwitcher.levelNum]);
            }
        }
    }

}