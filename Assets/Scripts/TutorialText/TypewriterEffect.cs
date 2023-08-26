using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
   [SerializeField] private float writingSpeed = 50;

    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        {new HashSet<char>(){'.', '!', '?'}, 0.2f },
        {new HashSet<char>(){',', ';', ':'}, 0.1f },
    };

    public Coroutine Run(string TextToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(TextToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;
        //yield return new WaitForSeconds(2);

        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            if (charIndex != 0 && Input.GetKeyDown(KeyCode.H))
            {
                textLabel.text = textToType.Substring(0, textToType.Length);
                charIndex = textToType.Length;
            }
            else
            {
                int lastCharIndex = charIndex;

                t += Time.deltaTime * writingSpeed;
                charIndex = Mathf.FloorToInt(t);
                charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);


                for (int i = lastCharIndex; i < charIndex; i++)
                {
                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        textLabel.text = textToType.Substring(0, textToType.Length);
                        charIndex = textToType.Length;
                    }

                    else
                    {
                        bool isLast = i >= textToType.Length - 1;

                        if (!isLast && textToType[i + 1] == '(')
                        {
                            //yield return new WaitForSeconds(1.0f);
                        }

                        if (Input.GetKeyDown(KeyCode.H))
                        {
                            textLabel.text = textToType.Substring(0, textToType.Length);
                            charIndex = textToType.Length;
                        }

                        textLabel.text = textToType.Substring(0, i + 1);

                        if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                        {
                            yield return new WaitForSeconds(waitTime);
                        }
                    }
                }
            }

            yield return null;
        }

        textLabel.text = textToType;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach(KeyValuePair<HashSet<char>, float> punctuationCategory in punctuations)
        {
            if (punctuationCategory.Key.Contains(character))
            {
                waitTime = punctuationCategory.Value;
                return true;
            }

        }

        waitTime = default;
        return false;
    }
}
