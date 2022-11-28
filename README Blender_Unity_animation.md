
# MakeHuman.
Created human didn't spend a lot of time for body but paid attention of face and wanted that it looks like me.
![image](https://user-images.githubusercontent.com/23377301/196936546-c065bce1-b8d3-4096-b224-28c945a25417.png)

# Blender
Issues. Created some other avatars with clothing and caused issue when exported to Blender!
[Screenshot (6)](https://user-images.githubusercontent.com/23377301/196937098-1d0be27e-c5db-46fe-b0ac-3330bc41ce2e.png)
When working on weight painting it was good practice because it took some time tom understand that need to associate to the bone.
Used some Youtube videos to understand how weight painting works.
https://youtu.be/l9S-jhBIEiU
# Animation Walk.
Watched some video how animation works 
https://youtu.be/nRtT7Gr6S2o

# Unity
When imported to the Unity it caused bad weighting when my avatar jumped the legg was making strange transform.
![Screenshot (7)](https://user-images.githubusercontent.com/23377301/196938032-06e5adcd-140a-44b5-9a02-6bea42c08ecc.png)

# Blend tree
Created blend tree but first time didn't worked and after research did some simple Blendtree and it works just little strange way.


https://user-images.githubusercontent.com/23377301/204005338-3cde1054-4a22-4435-9409-581cb54715bd.mp4

# Ragdoll and scripts
Created ragdoll for my Human and created some ombies. Able to shoot them with mouse and ragdoll activated.[Ragdoll_scripts2.zip](https://github.com/AndrisOrna/Blender/files/10093529/Ragdoll_scripts2.zip)



https://user-images.githubusercontent.com/23377301/204006507-54ab6275-eb88-482f-b29a-066162fe971d.mp4
# Shoot script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot2 : MonoBehaviour
{
    

    [SerializeField] private float _maximumForce;

    [SerializeField] private float _maximumForceTime;

    private float _timeMouseButtonDown;

    private Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _timeMouseButtonDown = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                SpaceBarPressed human = hitInfo.collider.GetComponentInParent<SpaceBarPressed>();

                if (human != null)
                {
                    float mouseButtonDownDuration = Time.time - _timeMouseButtonDown;
                    float forcePercentage = mouseButtonDownDuration / _maximumForceTime;
                    float forceMagnitude = Mathf.Lerp(1, _maximumForce, forcePercentage);

                    Vector3 forceDirection = human.transform.position - _camera.transform.position;
                    forceDirection.y = 1;
                    forceDirection.Normalize();

                    Vector3 force = forceMagnitude * forceDirection;

                    // getting function from SpaceBarPressed script
                    human.TriggerRagdoll(force, hitInfo.point);
                }
            }
        }
    }
}


# Shoot script

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

