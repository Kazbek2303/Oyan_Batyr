using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealFire : MonoBehaviour
{
    public float damage = 1f; // Amount of damage to deal
    public float cooldown = 1f; // Cooldown period between damage applications
    public float lifeTime = 2f;
    private bool canDealDamage = true; // Flag to check if damage can be dealt

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the projectile after a certain time
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDealDamage && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                StartCoroutine(DamageCooldown());
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(cooldown);
        canDealDamage = true;
    }

}
