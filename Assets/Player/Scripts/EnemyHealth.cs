using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    EnemyController enemyController;

    int health = 4;
    bool isDead = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemyController = GetComponent<EnemyController>();
    }

    void Update()
    {
        if (health < 1 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Batyr"))
        {
            health -= 1;
            Debug.Log(health);
        }
    }

    void Die()
    {
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
            anim.SetBool("IsDead", true);
        }

        // Optionally, disable the collider to prevent further interactions
        Collider mainCollider = GetComponent<Collider>();
        if (mainCollider != null)
        {
            mainCollider.enabled = false;
        }
    }
}
