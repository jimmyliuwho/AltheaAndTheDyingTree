using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDimensionalAnimationStateController : MonoBehaviour
{
    Animator animator;
    public float velocityZ = 0.0f;
    public float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        //Animator animator;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //get key input from the player if the key is pressed
        /*
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");*/
        bool runPressed = Input.GetKey("left shift");

        bool forwardPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w");
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a");
        bool rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d");
        bool backPressed = Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s");

        if (forwardPressed)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        if (backPressed)
        {
            //velocityZ = -1.0f;
            //Debug.Log("backPressed");
            velocityZ -= Time.deltaTime * acceleration;
        }

        if (leftPressed)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        if (rightPressed)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        //decrease VelZ
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        //reset Velocity X 
        if (!forwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        /*        //decrease VelZ
                if (!forwardPressed && !backPressed)
                {
                    if (velocityZ > 0.0)
                    {
                        velocityZ += Time.deltaTime * deceleration;
                    }
                    else
                    {
                        velocityZ -= Time.deltaTime * deceleration;
                    }
                }*/

/*        //decrease VelZ
        if (!backPressed && velocityZ < 0.0f)
        {
            //Debug.Log("back 1");
            velocityZ -= Time.deltaTime * deceleration;
        }

        //reset Velocity X 
        if (!backPressed && velocityZ > 0.0f)
        {
            //Debug.Log("back 2");
            velocityZ = 0.0f;
        }*/

        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);
    }
}
