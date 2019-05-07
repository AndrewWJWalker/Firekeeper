using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public List<GameObject> enemies;
    public List<NavMeshAgent> agents;
    public float speed = 0.3f;
    
    public bool initialised = false;

    void Start()
    {
        
    }

    void Update()
    {
        
        foreach(NavMeshAgent agent in agents)
        {
            agent.velocity = (Vector3.zero - agent.transform.position) * speed;
        }

    }
}
