using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TutorialTextObject/TutorialTextData")]

public class TutorialTextObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] tutorialText;

    public string[] TutorialText => tutorialText;
}
