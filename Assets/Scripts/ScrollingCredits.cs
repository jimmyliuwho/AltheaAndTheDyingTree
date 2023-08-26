using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingCredits : MonoBehaviour
{
    public float scrollSpeed = 60.0f;

    private RectTransform textRectTransform;

    private void Start()
    {
        textRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 position = textRectTransform.position;
        position.y += scrollSpeed * Time.deltaTime;
        textRectTransform.position = position;
    }
}
