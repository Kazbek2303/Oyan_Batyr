using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDied : MonoBehaviour
{
    private EnemyHealth enemy;
    public float flyUpSpeed = 2f;  
    public float flyUpDuration = 20f;

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
