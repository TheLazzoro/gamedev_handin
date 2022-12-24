using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarYeeter : MonoBehaviour
{
    [SerializeField] float OnScore_explosionRadius = 300f;
    [SerializeField] float OnScore_force = 7000f;
    HashSet<Rigidbody> affectedObjects;

    void Start()
    {
        affectedObjects = new HashSet<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var rigidBody = other.GetComponent<Rigidbody>();
        affectedObjects.Add(rigidBody);
    }

    private void OnTriggerExit(Collider other)
    {
        var rigidBody = other.GetComponent<Rigidbody>();
        affectedObjects.Remove(rigidBody);
    }

    /// <summary>
    /// Yeets all cars within the area away from the goal.
    /// </summary>
    public void Yeet()
    {
        foreach (var obj in affectedObjects)
        {
            obj.AddExplosionForce(OnScore_force, transform.position, OnScore_explosionRadius);
            Debug.Log(obj.ToString());
        }
    }
}
