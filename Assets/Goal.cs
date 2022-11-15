using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    GameObject agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ball") return;
        Debug.Log("Scored a goal on " + gameObject.tag);
        FindObjectOfType<ScoreManager>().AddPointToTeam(gameObject.tag);
    }
}
