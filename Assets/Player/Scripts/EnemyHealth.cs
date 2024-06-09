using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    EnemyController enemyController;

    public bool isDead = false;

    [SerializeField] float health, maxHealth = 3f;
    [SerializeField] public AudioSource GetHit;
    [SerializeField] public AudioSource Died;
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        GetHit.Play();
        anim.SetTrigger("Hit");

        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    private void Start()
    {
        health = maxHealth;
        anim = GetComponentInChildren<Animator>();
        agent = GetComponentInChildren<NavMeshAgent>();
        enemyController = GetComponentInChildren<EnemyController>();
    }

    void Die()
    {
        Died.Play();
        // Disable the enemy controller and navigation
        if (agent != null)
        {
            agent.enabled = false;
        }

        if (enemyController != null)
        {
            enemyController.enabled = false;
        }

        // Play the death animation
        if (anim != null)
        {
            anim.SetTrigger("isDead");
        }

        // Optionally, disable the collider to prevent further interactions
        Collider mainCollider = GetComponent<Collider>();
        if (mainCollider != null)
        {
            mainCollider.enabled = false;
        }
    }
}
