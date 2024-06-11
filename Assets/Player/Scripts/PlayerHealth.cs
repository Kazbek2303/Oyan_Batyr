using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class PlayerHealth : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    ThirdPersonController person;
    CharacterController characterController;
    [SerializeField] Animator enemyAnim;
    [SerializeField] private AudioSource GetHit;

    bool isDead = false;

    [SerializeField] public float health, maxHealth = 3f;
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        anim.SetTrigger("hit");
        GetHit.Play();

        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    private void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        person = GetComponent<ThirdPersonController>();
    }

    void Die()
    {
        
        // Disable the enemy controller and navigation
        if (agent != null)
        {
            agent.enabled = false;
        }

        if (characterController != null)
        {
            characterController.enabled = false;
        }

        if (person != null)
        {
            person.enabled = false;
        }

        // Play the death animation
        if (anim != null)
        {
            anim.SetTrigger("isDead");
            //enemyAnim.SetTrigger("killed");
        }
        
        //enemyAnim.SetTrigger("killed");
        // Optionally, disable the collider to prevent further interactions
        /* Collider mainCollider = GetComponent<Collider>();
         if (mainCollider != null)
         {
             mainCollider.enabled = false;
         }*/
    }

    public void EnemyCelebrate()
    {
        StartCoroutine(Celebrate());
    }

    private IEnumerator Celebrate()
    {
        yield return new WaitForSeconds(0f);

        if (enemyAnim != null) enemyAnim.SetTrigger("killed");
    }
}
