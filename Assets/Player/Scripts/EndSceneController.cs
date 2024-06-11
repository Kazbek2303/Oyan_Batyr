using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneController : MonoBehaviour
{
    public Image darkScreen;
    public float fadeDuration = 2f;
    public string restartSceneName;
    public float initialDelay = 30f;

    private void Start()
    {
        SetAlpha(darkScreen, 0);

        StartCoroutine(DelayedShowEndScreen(initialDelay));
    }

    private IEnumerator DelayedShowEndScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowEndScreen(); 
    }
    
    private void ShowEndScreen()
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

            yield return null;
        }

        yield return new WaitForSeconds(3f);

        RestartGame();
    }

    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(restartSceneName);
    }
}
