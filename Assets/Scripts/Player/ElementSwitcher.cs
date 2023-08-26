using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ElementSwitcher : MonoBehaviour
{
    public string element;

    void OnTriggerEnter(Collider other)
    {
        PlayerController c = other.GetComponentsInChildren<PlayerController>()[0];
        if (c != null)
        {
            c.ActivatePower(element);
        }else
        {
            ThirdPersonController tpc = other.GetComponent<ThirdPersonController>();
            tpc.ActivatePower(element);
        }
    }
}
