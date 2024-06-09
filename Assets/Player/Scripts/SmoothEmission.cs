using System.Collections;
using UnityEngine;

public class SmoothEmission : MonoBehaviour
{
    public float emissionIntensity = 1.0f; // The maximum emission intensity
    public float emissionDuration = 2.0f; // Duration of the emission transition

    private Coroutine emissionCoroutine;
    private Renderer objectRenderer;
    private Material[] materials;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("Renderer not found on the GameObject!");
            return;
        }

        materials = objectRenderer.materials;
        foreach (var mat in materials)
        {
            if (mat != null)
            {
                mat.EnableKeyword("_EMISSION");
            }
        }
    }

    public void StartEmission()
    {
        if (emissionCoroutine != null)
        {
            StopCoroutine(emissionCoroutine);
        }
        emissionCoroutine = StartCoroutine(EnableEmission());
    }

    private IEnumerator EnableEmission()
    {
        float elapsedTime = 0f;
        Color startColor = Color.black;
        Color endColor = Color.white * emissionIntensity;

        while (elapsedTime < emissionDuration)
        {
            elapsedTime += Time.deltaTime;
            float lerpValue = Mathf.Clamp01(elapsedTime / emissionDuration);

            Color currentEmissionColor = Color.Lerp(startColor, endColor, lerpValue);
            foreach (var mat in materials)
            {
                if (mat != null)
                {
                    mat.SetColor("_EmissionColor", currentEmissionColor);
                }
            }

            yield return null;
        }

        // Ensure the emission is fully enabled at the end
        foreach (var mat in materials)
        {
            if (mat != null)
            {
                mat.SetColor("_EmissionColor", endColor);
            }
        }
    }
}
