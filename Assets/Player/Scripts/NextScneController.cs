using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScneController : MonoBehaviour
{
    public Image darkScreen;
    public Text loadingText;
    public float fadeDuration = 2f;

    [SerializeField] private InventoryManager inventory;

    private void Start()
    {
        // Ensure the screen and text are fully transparent at the start
        SetAlpha(darkScreen, 0);
        SetAlpha(loadingText, 0); 
    }

    private void Update()
    {
        if(inventory.itemsAddedCount == 3)
        {
            CollectedThree();
        }
    }

    private void CollectedThree()
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
            SetAlpha(loadingText, alpha);

            yield return null;
        }

        yield return new WaitForSeconds(5f);

        LoadNextScene();

    }

    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
