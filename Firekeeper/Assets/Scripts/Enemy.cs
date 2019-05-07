using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    public void Die()
    {
        //play animation/trigger particles, then call this
        Destroy(this);
    }

    public void OnCollisionEnter(Collision collision)
    {
        //check object
        if (collision.collider.gameObject.CompareTag("Breakable"))
        {

        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
