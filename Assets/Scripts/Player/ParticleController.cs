using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
       particle = GetComponent<ParticleSystem>();
    }
    public void EnableParticles(Color color)
    {
        GetComponent<ParticleSystemRenderer>().material.color = color;
        particle.Play();
    }
}
