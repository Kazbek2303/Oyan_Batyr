using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public LayerMask interactableLayer;

    Collider[] colliders;
    [SerializeField] Image eButton;

    private List<IInteractable> inventory = new List<IInteractable>();
    private Coroutine fadeCoroutine;
    private bool isInRange = false;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, InteractRange);
    }

    void Start()
    {
        SetButtonAlpha(0f);
    }

    void Update()
    {
        colliders = Physics.OverlapSphere(InteractorSource.position, InteractRange, interactableLayer);

        bool hasInteractable = colliders.Length > 0;

        if (hasInteractable != isInRange)
        {
            isInRange = hasInteractable;

            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(FadeEButton(isInRange ? 1f : 0f));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
         
            CheckInteracte();
        }
    }

    private void CheckInteracte()
    {
        foreach (Collider collider in colliders)
        {
          
            if (collider.TryGetComponent<IInteractable>(out var interactObj))
            {
                if (IsClose(collider) &&  !inventory.Contains(interactObj))
                {
                    
                    interactObj.Interact();  
                    inventory.Add(interactObj);
                }
            }
        }
    }

    bool IsClose(Collider collider)
    {
        float distance = Vector3.Distance(InteractorSource.position, collider.transform.position);
        return distance <= InteractRange;
    }
    IEnumerator FadeEButton(float targetAlpha)
    {
        float startAlpha = eButton.color.a;
        float duration = 0.5f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            Color color = eButton.color;
            color.a = alpha;
            eButton.color = color;
            yield return null;
        }

        Color finalColor = eButton.color;
        finalColor.a = targetAlpha;
        eButton.color = finalColor;
    }

    void SetButtonAlpha(float alpha)
    {
        Color color = eButton.color;
        color.a = alpha;
        eButton.color = color;
    }

}
