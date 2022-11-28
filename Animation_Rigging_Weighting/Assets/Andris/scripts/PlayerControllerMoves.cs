using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControllerMoves : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;
    Animator anim;

    private PlayerInput Input;
    // Start is called before the first frame update
    void Start()
    {
        Input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 keyMove = context.ReadValue<Vector2>();
        velocity = new Vector3(keyMove.x, 0.0f, keyMove.y);
    }

    private void FixedUpdate()
    {
        rb.velocity= 2.0f * velocity;
        Vector3 direction = Vector3.RotateTowards(transform.forward, velocity, 0.2f, 0.0f);
        rb.rotation = Quaternion.LookRotation(direction);
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", velocity.magnitude);
    }
    private void OnDisable()
    {
        Input.actions = null;
    }
}
