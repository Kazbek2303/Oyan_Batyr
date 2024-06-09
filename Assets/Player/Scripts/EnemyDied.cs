using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDied : MonoBehaviour
{
    private EnemyHealth enemy;
    public float flyUpSpeed = 2f;  // Speed at which the enemy flies up
    public float flyUpDuration = 5f;  // Duration of the fly up effect

    private void Start()
    {
        enemy = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (enemy.isDead)
        {
            StartCoroutine(FlyUp());
            enabled = false;  // Disable this script to prevent multiple coroutines
        }
    }

    private IEnumerator FlyUp()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < flyUpDuration)
        {
            transform.position = startPosition + Vector3.up * flyUpSpeed * (elapsedTime / flyUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position is fully up
        transform.position = startPosition + Vector3.up * flyUpSpeed;
    }
}
