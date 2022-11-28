using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;
    private float velocityX = 0.0f;
    private float velocityZ = 0.0f;

    [SerializeField] private float acceleration = 2.0f;
    [SerializeField] private float deceleration = 2.0f;
    //[SerializeField] private GameObject flipSounds;
    //[SerializeField] private GameObject coin;
    //[SerializeField] private ParticleSystem confettie;
    //[SerializeField] private Transform playerFeet;
    //[SerializeField] private Transform butt;

    public bool dontAnimateWalk = false;
    private bool dontanimatewalk;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool backPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool g_Pressed = Input.GetKey("g");
        //bool twoPressed = Input.GetKey("2");

        if (!dontAnimateWalk)
        {
            if (forwardPressed && velocityZ < 0.5f)
            {
                velocityZ += Time.deltaTime * acceleration;
            }

            if (!forwardPressed && velocityZ > 0.0f)
            {
                velocityZ -= Time.deltaTime * deceleration;
            }

            if (backPressed && velocityZ > -0.5f)
            {
                velocityZ -= Time.deltaTime * acceleration;
            }

            if (!backPressed && velocityZ < -0.0f)
            {
                velocityZ += Time.deltaTime * deceleration;
            }

            if (leftPressed && velocityX > -0.5f)
            {
                velocityX -= Time.deltaTime * acceleration;
            }

            if (rightPressed && velocityX < 0.5f)
            {
                velocityX += Time.deltaTime * acceleration;
            }

            if (!leftPressed && velocityX < 0.0f)
            {
                velocityX += Time.deltaTime * deceleration;
            }

            if (!rightPressed && velocityX > 0.0f)
            {
                velocityX -= Time.deltaTime * deceleration;
            }

            updateAnimator();
            //ResetVelocityForward(forwardPressed);
            ResetVelocityLeftAndRight(leftPressed, rightPressed);
        }

        if (g_Pressed && !dontanimatewalk)
        {
            gestureAnimation();
        }
    }

    private void updateAnimator()
    {
        animator.SetFloat("velocityX", velocityX);
        //Debug.Log("VelocityX: " + velocityX);
        animator.SetFloat("velocityZ", velocityZ);
        //Debug.Log("VelocityZ: " + velocityZ);
    }

    private void ResetVelocityForward(bool forwardPressed)
    {
        if (!forwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }
    }

    private void ResetVelocityLeftAndRight(bool leftPressed, bool rightPressed)
    {
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }
    }

    private void gestureAnimation()
    {
        dontanimatewalk = true;
        animator.Play("gesture");
    }


    public void AllowInput()
    {
        dontAnimateWalk = false;
    }
}
