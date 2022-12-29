using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hit;
    public AudioClip explosion;

    private Rigidbody rigidbody;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            rigidbody.velocity = Vector3.zero; // reset ball position
            Vector3 carVelocity = other.GetComponent<Rigidbody>().velocity;
            Vector3 force = carVelocity * 100;
            rigidbody.AddForce(force); // apply custom force
        }

        if (other.name != "GoalTriggerArea")
        {
            audioSource.PlayOneShot(hit);
        } else {
            audioSource.PlayOneShot(explosion);
        }
    }
}
