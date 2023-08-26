using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColorOfTrees : MonoBehaviour
{

    public Color colorStart;
    public Color colorEnd;
    public int index;
    Renderer rend;

    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        rend.materials[index].color = colorStart;
    }

    void Update()
    {
        float lerp = LevelSwitcher.levelNum / 6;
        rend.materials[index].color = Color.Lerp(colorStart, colorEnd, lerp);
    }
}
