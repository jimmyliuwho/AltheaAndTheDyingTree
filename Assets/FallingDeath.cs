using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FallingDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "PLAYER FINAL")
        {
            SceneManager.LoadScene("Death");
        }
    }
}
