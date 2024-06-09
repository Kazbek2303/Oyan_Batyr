using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenControlle : MonoBehaviour
{
    public Image darkScreen;
    public Text deathText;
    public float fadeDuration = 2f;

    private void Start()
    {
        // Ensure the screen and text are fully transparent at the start
        SetAlpha(darkScreen, 0);
        SetAlpha(deathText, 0);
    }

    public void ShowDeathScreen()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            SetAlpha(darkScreen, alpha);
            SetAlpha(deathText, alpha);

            yield return null;
        }

        yield return new WaitForSeconds(5f);

        RestartLevel();

    }

    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
