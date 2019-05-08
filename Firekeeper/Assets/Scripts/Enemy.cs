using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSG.MeshAnimator;

public class Enemy : MonoBehaviour
{

    public int strength = 1;
    public float attackCooldown = 3;
    public float attackRange = 0.5f;
    FSG.MeshAnimator.MeshAnimator animator;

    Health targetHealth;
    Transform target;

    bool canAttack = true;
    public bool alive = true;

    public void StartDeath()
    {
        StartCoroutine(DeathAnimation());
        alive = false;
    }

    IEnumerator DeathAnimation()
    {
        animator.Play(1);
        yield return new WaitForSeconds(3);
        Die();
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
        Debug.Log("doin a hit");
        targetHealth.DealDamage(strength);
        canAttack = false;
        StartCoroutine(AttackCooldown());
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
