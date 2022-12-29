using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    float duration;

    void Start()
    {
        GameObject sparks = GameObject.Find("Sparks");
        particleSystem = sparks.GetComponent<ParticleSystem>();
        particleSystem.Stop();
        
    }

    void Update()
    {
        if (duration < 0)
            particleSystem.Stop();
        else
            duration -= Time.deltaTime;
    }

    public void TriggerExplosion()
    {
        particleSystem.Play();
        duration = particleSystem.duration;
        FindObjectOfType<SFXHandler>().playSFX("explosion", 1f);
    }
}
