using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrees : MonoBehaviour
{


    public List<GameObject> trees;
    public int treeCount;
    public float x;
    public float z;
    public float campRange;

    void Start()
    {
        for (int loop = 0; loop < treeCount; loop++)
        {
            Vector3 position = Vector3.zero;
            bool invalid = true;
            int tries = 0;

            while (invalid)
            {
                tries++;
                position.x = Random.Range(-x, x);
               
                position.z = Random.Range(-z, z);
                if (position.x > campRange || position.x < -campRange)
                {
                    if (position.z > campRange || position.z < -campRange)
                    {
                        invalid = false;
                    }
                }
                if (tries > 100)
                {
                    invalid = false;
                }
            }
            int index = Random.Range(0, trees.Count);
            GameObject tree = Instantiate(trees[index], this.transform);
            tree.transform.position = position;
            Vector3 rotation = tree.transform.eulerAngles;
            rotation.y = Random.Range(0, 360);
            tree.transform.eulerAngles = rotation;
            
        }
    }


}
