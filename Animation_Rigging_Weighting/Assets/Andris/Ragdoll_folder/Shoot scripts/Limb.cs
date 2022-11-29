using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    [SerializeField] Limb[] ChildLimbs;
    [SerializeField] GameObject limbPrefab;
    [SerializeField] GameObject woundHole;

    // Start is called before the first frame update
    void Start()
    {
        if (woundHole != null)
        {
            woundHole.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetHit()
    {
        if (ChildLimbs.Length > 0)
        {
            foreach(Limb limb in ChildLimbs)
            {
                if(limb != null)
                {
                    limb.GetHit();
                }
            }
        }
        if(woundHole != null)
        {
            woundHole.SetActive(true);
        }
        if(limbPrefab != null)
        {
            Instantiate(limbPrefab, transform.position, transform.rotation);
        }
        // scalling limb to zero, resize
        transform.localScale = Vector3.zero;
        Destroy(this);
    }
}
