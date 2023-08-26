using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath2 : MonoBehaviour
{
    public GameObject player;

    void Update() {
        if (transform.position.y < 120 && SceneManager.GetActiveScene().name == "TutorialAllPowers")
        {
            Debug.Log("fell off");
            Destroy(player);
            SceneManager.LoadScene("Death2");
        }
    }
    

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //Debug.Log("hit by enemy");
            Destroy(player);
            SceneManager.LoadScene("Death2");
        }
    }


}
