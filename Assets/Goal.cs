using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class Goal : MonoBehaviour
{
    [SerializeField] string teamName;
    [SerializeField] float OnScore_explosionRadius = 300f;
    [SerializeField] float OnScore_force = 7000f;
    GameObject agent;
    bool activated;
    float pauseTime = 3f;


    void Start()
    {
        agent = GetComponent<GameObject>();
        activated = true;
    }

    void Update()
    {   
        if (activated == false) {
        pauseTime -= Time.deltaTime;
            if (pauseTime <= 0) {
                RestartScene restartscene = new RestartScene();
                restartscene.ResetScene();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activated || other.tag != "Ball") return;
        var ball = other.GetComponent<Transform>();
        activated = false; // TODO: Remember to re-activate the goal on round reset.
        Debug.Log("Scored a goal on " + teamName);
        FindObjectOfType<ScoreManager>().AddPointToTeam(gameObject.tag);

        // Explosion
        var ballEx = other.GetComponent<Explosion>();
        ballEx.TriggerExplosion();
        var affectedObjects = Physics.OverlapSphere(transform.position, OnScore_explosionRadius);
        foreach (var obj in affectedObjects)
        {
            var rigidBody = obj.GetComponent<Rigidbody>();
            if (rigidBody == null || obj.tag == "Ball")
                continue;

            rigidBody.AddExplosionForce(OnScore_force, ball.position, OnScore_explosionRadius);
            Debug.Log(rigidBody.ToString());
        }
    }
}
