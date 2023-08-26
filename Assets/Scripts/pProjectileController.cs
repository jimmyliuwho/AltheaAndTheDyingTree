using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pProjectileController : MonoBehaviour
{

    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private float projectileSpeed;
    public PlayerController.ElementState element;

    private float timeAlive;
    [SerializeField] private float timeUntilDespawn = 3.5f;


    // Start is called before the first frame update
    void Start()
    {
        timeAlive = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += (movementDirection * projectileSpeed * Time.deltaTime);
        timeAlive += Time.deltaTime;
        if (timeAlive >= timeUntilDespawn) {
            Destroy(this.gameObject);
        }
    }

    public void SetDirection(Vector3 direction) {
        movementDirection = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger");
        //gameObject.SetActive(false);
        //Debug.Log(other);
        if (other.tag == "FlowerToGrow")
        { 
            //other.gameObject.GetComponent<TriggerGrowth>().Grow();
        }
    }

}
