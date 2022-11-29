using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem : MonoBehaviour
{
    [Header("Unity setup")]

    public ParticleSystem deathParticles;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Zombie"))
        {
            Destroy();
        }
    }
    public void Destroy()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
