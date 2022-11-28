using System.Linq; // allows to easily query collections, such as our collection of Rigidbodies
using UnityEngine;

public class SpaceBarPressed : MonoBehaviour
{
    private enum HumanState
    {
        Idle,
        Ragdoll
    }

    //[SerializeField] private Camera _camera;
    private Rigidbody[] _ragdollRigidbodies;
    private HumanState _currentState = HumanState.Idle;
    private Animator _animator;
    private CharacterController _characterController;
    
    void Awake()
    {
        // getting aniamtor for character
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        Disableragdoll();
    }

    //Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case HumanState.Idle:
                IdleBehaviour();
                break;
            case HumanState.Ragdoll:
                RagdollBehaviour();
                break;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            EnableRagdoll();
        }
        
    }

    private void Disableragdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        _animator.enabled = true;
        _characterController.enabled = true;

    }

    private void EnableRagdoll()
        {
         foreach (var rigidbody in _ragdollRigidbodies)
            {
             rigidbody.isKinematic = false;
            }
         _animator.enabled = false;
        _characterController.enabled = false;

        }
    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdoll();
        Rigidbody hitRigidbody = _ragdollRigidbodies.OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitPoint)).First();
        hitRigidbody.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);
        _currentState = HumanState.Ragdoll;

    }
    private void IdleBehaviour()
    {
        //Vector3 direction = _camera.transform.position - transform.position;
        //direction.y = 0;
        //direction.Normalize();

        //Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 20 * Time.deltaTime);
    }

    private void RagdollBehaviour()
    {

    }
}