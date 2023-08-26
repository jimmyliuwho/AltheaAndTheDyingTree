using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TriggerDeath : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
            Debug.Log("Enemy Death triggered");
            //anim.SetBool("isDead", true);
            //gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            //Destroy(agent, 2);
        
    }

    private void Update()
    {
        
    }
}
