using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSkeleton : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            Debug.Log("HIT PLAYER!");
            player.TakeDamage(damage);
        }
    }
}
