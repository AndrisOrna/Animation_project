using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    Animator myAnim;
    List<Rigidbody> ragdollRigids;
    void Start()
    {

        myAnim = GetComponent<Animator>();
        // finding rigidbodys
        ragdollRigids = new List<Rigidbody>(transform.GetComponentsInChildren<Rigidbody>());
        ragdollRigids.Remove(GetComponent<Rigidbody>());

        DeActivateRagdoll();
    }

    void Update()
    {

    }

    void ActivateRagdoll()
    {
        myAnim.enabled = false;
        for (int i = 0; i < ragdollRigids.Count; i++)
        {
            ragdollRigids[i].useGravity = true;
            ragdollRigids[i].isKinematic = false;
        }
    }void DeActivateRagdoll()
    {
        myAnim.enabled = true;
        for (int i = 0; i < ragdollRigids.Count; i++)
        {
            ragdollRigids[i].useGravity = false;
            ragdollRigids[i].isKinematic = true;
        }
    }
}
    













//public Collider MainCollider;
//public Collider[] AllColliders;
//// Start is called before the first frame update
//void Start()
//{
//    MainCollider = GetComponent<Collider>();
//    AllColliders = GetComponentsInChildren<Collider>(true);
//}

//public void DoRagdoll(bool isRagdoll)
//{
//    // iterating in all colliders
//    foreach (var col in AllColliders)
//        col.enabled = isRagdoll;  // collider enabled when in ragdoll mode
//    MainCollider.enabled = !isRagdoll;
//    GetComponent<Rigidbody>().useGravity = !isRagdoll;
//    GetComponent<Animator>().enabled = !isRagdoll;
//}
//// Update is called once per frame
//void Update()
//{

//}