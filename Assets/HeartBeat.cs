using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.Find("Doorway");
        door.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward * 50 * Time.deltaTime, Space.Self);
        if (LevelSwitcher.globalPineCount == 0)
        {
            door.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pinecone")
        {
            gameObject.transform.parent.position += new Vector3(0, 0.1f, 0);
            gameObject.transform.localScale += new Vector3(3, 3, 3);
        }
    }
}
