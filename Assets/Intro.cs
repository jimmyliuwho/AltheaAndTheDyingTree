using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{

    Image menuImage;
    byte transition;
    bool trans;
    private int time;

    byte t1;
    byte t2;
    byte t3a;
    byte t3b;
    byte t3c;
    byte t4;
    byte t5;
    byte t6;

    // Start is called before the first frame update
    void Start()
    {
        menuImage = GetComponent<Image>();
        transition = 254;
        trans = false;
        time = 0;

        t1 = 0;
        t2 = 0;
        t3a = 0;
        t3b = 0;
        t3c = 0;
        t4 = 0;
        t5 = 0;
        t6 = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!trans)
        {
            menuImage.color = new Color32(transition, transition, transition, transition);
            transition -= 2;
        }
        else
        {
            if (time > 100 && t1 < 254) t1 += 2;
            if (time > 240 && t2 < 254) t2 += 2;
            if (time > 375 && t3a < 254) t3a += 2;
            if (time > 435 && t3b < 254)
            {
                t3b += 2;
                t3a -= 2;
            }
            if (time > 495 && t3c < 254)
            {
                t3c += 2;
                t3b -= 2;
            }
            if (time > 640 && t4 < 254) t4 += 2;
            if (time > 770 && t5 < 254) t5 += 2;
            //if (time > 800 && t6 < 245) t6 += 8;

        }
        if (transition == 0)
        {
            trans = true;
            transition -= 1;
        }
        time++;

        GameObject.Find("IntroText1").GetComponent<Text>().color = new Color(1, 1, 1, t1 / 255f);
        GameObject.Find("IntroText2").GetComponent<Text>().color = new Color(1, 1, 1, t2 / 255f);
        GameObject.Find("IntroText3").GetComponent<Text>().color = new Color(1, 1, 1, t3a / 255f);
        GameObject.Find("IntroText3.1").GetComponent<Text>().color = new Color(1, 1, 1, t3b / 255f);
        GameObject.Find("IntroText3.2").GetComponent<Text>().color = new Color(1, 1, 1, t3c / 255f);
        GameObject.Find("IntroText4").GetComponent<Text>().color = new Color(1, 1, 1, t4 / 255f);
        GameObject.Find("IntroText5").GetComponent<Text>().color = new Color(1, 1, 1, t5 / 255f);
        //Debug.Log(GameObject.Find("Start").GetComponent<Button>().colors);
        //GameObject.Find("Start").GetComponent<Image>().color = new Color(1, 1, 1, t6 / 255f);
        //GameObject.Find("StartText").GetComponent<Text>().color = new Color(0.2f, 0.2f, 0.2f, t6 / 255f);

    }
}
