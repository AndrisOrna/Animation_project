using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Collider MainCollider;
    public Collider[] AllColliders;
    // Start is called before the first frame update
    void Start()
    {
        MainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
    }

    public void DoRagdoll(bool isRagdoll)
    {
        // iterating in all colliders
        foreach (var col in AllColliders)
            col.enabled = isRagdoll;  // collider enabled when in ragdoll mode
        MainCollider.enabled = !isRagdoll;
        GetComponent<Rigidbody>().useGravity = !isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
