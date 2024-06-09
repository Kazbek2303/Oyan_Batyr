using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDeathParticles : MonoBehaviour
{
    public ParticleSystem deathParticles; // Reference to the particle system
    private EnemyHealth enemyHealth;
    public SmoothEmission smoothEmission;

    private void Start()
    {
        deathParticles.Pause();
        enemyHealth = GetComponent<EnemyHealth>();
        if (deathParticles == null)
        {
            Debug.LogError("Death particles not assigned!");
        }

        if (smoothEmission == null)
        {
            Debug.LogError("SmoothEmission script not assigned!");
        }

    }

    private void Update()
    {
        if (enemyHealth.isDead)
        {
            if (!deathParticles.isPlaying)
            {
                PlayDeathParticles();
            }
            if (smoothEmission != null)
            {
                smoothEmission.StartEmission();
            }
            enabled = false; // Disable this script to prevent multiple calls
        }
    }

    private void PlayDeathParticles()
    {
        if (deathParticles != null)
        {
            deathParticles.Play();
        }
    }
}

