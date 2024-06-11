using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    public float letterDelay = 0.1f; // Delay between letters
    public Text textComponent; // Reference to the Text component

    private string fullText;

    void Start()
    {
        fullText = textComponent.text;
        textComponent.text = ""; // Clear the text initially
        StartCoroutine(RevealText());
    }

    IEnumerator RevealText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            textComponent.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(letterDelay);
        }

        // Enable the ability to load the next scene or quit the application after the text display
        StartCoroutine(LoadNextSceneOrQuit());
    }

    IEnumerator LoadNextSceneOrQuit()
    {
        while (true)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("Pressed");
                StartCoroutine(FadeOutText());
                yield return new WaitForSeconds(3f); // Delay before scene transition or quitting
                LoadNextOrQuit();
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator FadeOutText()
    {
        Color textColor = textComponent.color;
        float alpha = 1f;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * 1f;
            textColor.a = alpha;
            textComponent.color = textColor;
            yield return null;
        }
    }

    void LoadNextOrQuit()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Quit();
        }
    }

    void Quit()
    {
        Application.Quit();
    }
}
