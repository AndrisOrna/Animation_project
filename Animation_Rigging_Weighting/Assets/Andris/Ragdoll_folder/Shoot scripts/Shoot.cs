using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // creating ray
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
            var hits = Physics.RaycastAll(ray);
            foreach(var hit in hits)
            {
                var enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.DoRagdoll(true);
                }
            }
        }
    }
}
