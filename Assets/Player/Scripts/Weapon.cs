using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyComponent = other.gameObject.GetComponent<EnemyHealth>();
            Debug.Log("HIT!");
            enemyComponent.TakeDamage(damage);
        }
    }
}
