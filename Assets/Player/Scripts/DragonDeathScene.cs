using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragonDeathScene : MonoBehaviour
{
    public Image darkScreen;
    public float fadeDuration = 2f;
    public EnemyHealth enemyHealth;
    public float initialDelay = 2f;

    private bool hasStartedCoroutine = false;

    private void Start()
    {
        SetAlpha(darkScreen, 0);
    }

    private void Update()
    {
        if (enemyHealth.isDead && !hasStartedCoroutine)
        {
            hasStartedCoroutine = true;
            StartCoroutine(DelayedShowEndScreen(initialDelay));
        }
    }

    public void ShowEndScreen()
    {
        StartCoroutine(FadeIn());
    }
    private IEnumerator DelayedShowEndScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowEndScreen(); 
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            SetAlpha(darkScreen, alpha);

            yield return null;
        }

        yield return new WaitForSeconds(3f);

        LoadLastScene();
    }

    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
    private void LoadLastScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
