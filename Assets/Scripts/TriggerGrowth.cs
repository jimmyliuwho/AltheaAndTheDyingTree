using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TriggerGrowth : MonoBehaviour
{

    public void Start()
    {
        if(LevelSwitcher.levelNum == 2)
        {
            Grow();
        }
    }

    public void Grow()
    {
        //Debug.Log("Growth animation triggered");
        Animator anim = gameObject.GetComponent<Animator>();
        anim.Play("Grow");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("tag: "+other.gameObject.tag);
        //Debug.Log(other.gameObject.layer);
        if (other.gameObject.tag == "LightPower" && other.gameObject.layer == 9)
        {
            Grow();
        }
    }
}

