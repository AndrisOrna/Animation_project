using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController22222 : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;

    public float acceleration = 0.1f;
    public float decelaration = 0.5f;
    int VelocityMash;
    // Start is called before the first frame update
    void Start()
    {
        // set reference for animator
        animator = GetComponent<Animator>();
        // increase performance
        VelocityMash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        // get key input from player
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if (forwardPressed && velocity < 1.0f)
        {
            velocity += Time.deltaTime * acceleration;
            animator.SetFloat(VelocityMash, velocity);
        }
        if (!forwardPressed && velocity> 0.0f)
        {
            velocity -= Time.deltaTime * decelaration;
        }
        if (!forwardPressed && velocity < 0.0f)
        {
            velocity = 0.0f;
        }
        animator.SetFloat(VelocityMash, velocity);
    }
}
