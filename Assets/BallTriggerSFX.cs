using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTriggerSFX : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip hit;
    public AudioClip explosion;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "GoalTriggerArea")
        {
            audioSource.PlayOneShot(hit);
        } else {
            audioSource.PlayOneShot(explosion);
        }
    }
}
