using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
{
    public GameObject player;
    Animator animator;
    float countFrom;
    bool crash;
    private bool alreadyStarted;
    // these are for the transition screen
    public Animator transition;
    public float transitionTIme = 1.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        countFrom = 2.0f;
        crash = false;
        alreadyStarted = false;
    }

    void Update()
    {
        if (crash == true && !alreadyStarted)
        {
            Debug.Log("ah yes");
            StartCoroutine(LoadLevel());
            alreadyStarted = true;
            /* animator.SetBool("crashEnemy", true);*/
            /*//Debug.Log("");
            countFrom -= Time.deltaTime;
            //Debug.Log(countFrom);

            if (countFrom <= 0)
            {
                //Debug.Log("hit 0");
                animator.SetBool("crashEnemy", false);
                crash = false;
                ResetCount();
            }*/
        }
     /*   if (transform.position.y < 120 && SceneManager.GetActiveScene().name == "TutorialAllPowers") // check this part
        {
            //Debug.Log("fell off");
            Destroy(player);
            SceneManager.LoadScene("Death");
        }*/
    }

    void ResetCount()
    {
        countFrom = 2.0f;
    }

    void Countdown()
    {
        if (countFrom > 0)
        {
            countFrom -= Time.deltaTime;
        }
        if (countFrom == 0)
        {
            countFrom = 2.0f;
            animator.SetBool("crashEnemy", false);
        }
    }

    //void Update() {
    //if (transform.position.y < 120 && SceneManager.GetActiveScene().name == "TutorialAllPowers")
    //{
    //    Debug.Log("fell off");
    //    Destroy(player);
    //    SceneManager.LoadScene("Death");
    //}
    //}

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject);
        if (col.gameObject.tag == "Enemy")
        {
            crash = true;
            Debug.Log("hit by enemy");
            //falls on to the ground
            /*animator.SetBool("crashEnemy", true);*/
            /*StartCoroutine(LoadLevel());*/
            //SceneManager.LoadScene("Death");
        }
    }

    IEnumerator LoadLevel()
    {
        Debug.Log("potato");
        animator.SetBool("crashEnemy", true);
        //if (alreadyStarted) SceneManager.LoadScene("Death");

        //yield return new WaitForSeconds(transitionTIme);
        // play animation for transition
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTIme);
        //Load Scene
        //LevelSwitcher.globalPineCount += GameObject.Find("PLAYER FINAL").GetComponentInChildren<PlayerController>().pineconeCount;
        SceneManager.LoadScene("Death");


    }




}
