using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSG.MeshAnimator;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;

    public EnemyAI enemyManager;

    public float x;
    public float z;
    public float minRadius = 10;
    public int count;
    
    void Start()
    {

        enemyManager.initialised = true;
        StartCoroutine(Spawn());

    }

    IEnumerator Spawn()
    {

        for (int loop = 0; loop < count; loop++)
        {
            Vector3 position = transform.position;
            position.y = 0;
            position.x += Random.Range(-x, x);
            if (position.x > 0)
            {
                position.x += Random.Range(minRadius, minRadius * 2);
            }
            if (position.x < 0)
            {
                position.x -= Random.Range(minRadius, minRadius * 2);
            }
            position.z += Random.Range(-z, z);
            if (position.z > 0)
            {
                position.z += Random.Range(minRadius, minRadius * 2);
            }
            if (position.z < 0)
            {
                position.z -= Random.Range(minRadius, minRadius * 2);
            }
            GameObject newEnemy = Instantiate(enemy, this.transform);
            position.y = newEnemy.transform.position.y;
            newEnemy.transform.position = position;



            enemyManager.enemies.Add(newEnemy);
            enemyManager.agents.Add(newEnemy.GetComponent<NavMeshAgent>());
            yield return new WaitForEndOfFrame();
        }
    }



    void Update()
    {
        
    }
}
