using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public PlayerController playerController;
    public GameObject fallingDeath;

    // singleton design pattern for the GameManager
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (fallingDeath == null)
        {
            fallingDeath = GameObject.Find("FallingDeath");
        }
        if (playerController == null) {
            playerController = FindObjectOfType<PlayerController>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        // check for falling death
/*        if (playerController != null && !playerController.isGrounded) {
            if (playerController.gameObject.transform.position.y <= -42f) {
                SceneManager.LoadScene("Death");
            }
        }*/
    }

    public static void DieByFall()
    {
        SceneManager.LoadScene("Death");
    }




}
