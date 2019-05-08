using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int strength = 1;

    public void Die()
    {
        //play animation/trigger particles, then call this
        Destroy(this);
    }

    public void OnCollisionEnter(Collision collision)
    {
        //check object
        Health health = collision.collider.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.DealDamage(strength);
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
