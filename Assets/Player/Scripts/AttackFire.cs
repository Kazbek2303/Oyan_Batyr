using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFire : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 10f;
    public float lifeTime = 5f;
    public GameObject impactPrefab; // Prefab to instantiate on impact

    private Vector3 targetDirection;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the projectile after a certain time
    }

    private void Update()
    {
        // Move the projectile forward in the target direction
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    public void SetTarget(Vector3 targetPosition)
    {
        // Calculate the direction towards the target position
        targetDirection = (targetPosition - transform.position).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the projectile hits the player, apply damage
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        // Instantiate the impact prefab and destroy the projectile
        Instantiate(impactPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
