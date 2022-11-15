using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTriggerSFX : MonoBehaviour
{

    public AudioSource audioSource;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "GoalTriggerArea") audioSource.Play();
    }
}
