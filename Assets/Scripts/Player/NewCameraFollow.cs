using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class NewCameraFollow : MonoBehaviour
{

    [SerializeField] private float followRadialDistance;
    [SerializeField] private float followYDistance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        SetCameraPositionAndRotation();
    }

    // TODO - add clipping, add the distance pull thingy
    void SetCameraPositionAndRotation()
    {
        PlayerController pc = GameManager.Instance.playerController;
        if (pc != null)
        {
            Vector3 playerPos = pc.gameObject.transform.position;
            Vector3 cameraPos = playerPos;
            cameraPos.z += followRadialDistance;
            cameraPos.y += followYDistance;

            //Debug.Log(pc.gameObject.transform.rotation);

            gameObject.transform.position = cameraPos;
            gameObject.transform.rotation = pc.gameObject.transform.rotation;

            transform.LookAt(pc.gameObject.transform);

        }
        else // if no player controller, TODO: this could happen if player is dead, or cutscene

        {

        }
    }
}
