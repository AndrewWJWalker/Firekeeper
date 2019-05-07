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
            if (agent != null)
            {
                agent.velocity = (Vector3.zero - agent.transform.position) * speed;
            }
        }

    }

    public void Exterminate()
    {
        foreach( GameObject enemy in enemies )
        {
            Destroy(enemy);
        }
    }
}
