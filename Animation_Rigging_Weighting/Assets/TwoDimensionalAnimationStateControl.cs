using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalAnimationStateControl : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;

    // increase performance
    int VelocityZHash;
    int VelocityXHash;

    // Start is called before the first frame update
    void Start()
    {
        // search the gameObject this script is attached to and get the animator component
        animator = GetComponent<Animator>();
        // increase  performance
        VelocityZHash = Animator.StringToHash("VelocityZ");
        VelocityXHash = Animator.StringToHash("VelocityX");
    }

    // handles acceleration and deacceleration
    void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        // reset velocityZ
        if (!forwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        // increase velocityX if left is not pressed and velocityXX < 0
        if (!leftPressed && velocityX > 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        // decrease velocityX if right is not pressed and velocityX > 0
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        // reset velocityX
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }
    }
    void lockOrResetVelocity( bool forwardPressed,bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        // lock forward 
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            // round to the currentMaxVelocity if within offset
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // input will be true if the player is pressing on ht e passed in key parameter
        // get key input from player when button pressed
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // set current maxVelocity
        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        // handle changes in velocity
        changeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

        // setting the parameters to local variable values
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);

    }
}
