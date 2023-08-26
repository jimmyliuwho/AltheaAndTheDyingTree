using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDestroy : MonoBehaviour
{
    public GameObject enemy;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(enemy, 5);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if projectile collision 

        // then Destroy(enemy);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "WaterPower")
        {
            //anim.SetBool("isDead", true);
            //gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            //Debug.Log("hit by water");
            Destroy(enemy, 1);
        }
    }
}
