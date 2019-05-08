using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSG.MeshAnimator;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public int strength = 1;
    public float attackCooldown = 3;
    public float attackRange = 0.5f;
    [HideInInspector] public NavMeshAgent agent;
    FSG.MeshAnimator.MeshAnimator animator;

    Health targetHealth;
    Transform target;

    bool canAttack = true;
    public bool alive = true;

    public void StartDeath()
    {
        if (agent != null)
        {
            Destroy(agent);
        }
        StartCoroutine(DeathAnimation());
        alive = false;
    }

    IEnumerator DeathAnimation()
    {
        animator.Play(1);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SinkIntoGround());
        yield return new WaitForSeconds(5);
        Die();
    }

    IEnumerator SinkIntoGround()
    {
        bool doing = true;
        while (doing)
        {
            transform.position = transform.position + Vector3.down * 0.01f;
           // Debug.Log("Sinking dead dude");
            yield return new WaitForEndOfFrame();
        }
    }

    void Die()
    {
        //play animation/trigger particles, then call this
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        //check object
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            targetHealth = health;
            target = other.gameObject.transform;
        }
    }

    public void AttackHit()
    {
        animator.Play(2);
      //  Debug.Log("doin a hit");
        targetHealth.DealDamage(strength);
        canAttack = false;
        StartCoroutine(AttackCooldown());
        StartCoroutine(AttackAnimToIdle());
    }

    IEnumerator AttackAnimToIdle()
    {
        yield return new WaitForSeconds(1);
        animator.Play(0);
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<MeshAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.position) < attackRange)
                {
                    AttackHit();
                }
            }
        }
    }
}
