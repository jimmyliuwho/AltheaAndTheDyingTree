using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayPinecones : MonoBehaviour
{
    public Text PineconeText;
    // Start is called before the first frame update
    void Start()
    {
        PineconeText = GameObject.Find("PineconesText").GetComponent<Text>();
        PineconeText.text = "" + LevelSwitcher.globalPineCount;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
