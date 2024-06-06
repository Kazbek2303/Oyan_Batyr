using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool isInRange = false;

    private Animator animator;
    [SerializeField] Collider currentPickup;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the player!");
        }
    }

    void Update()
    {
        if (currentPickup != null && Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            // Trigger the pickup animation
            if (animator != null)
            {
                animator.SetTrigger("pickup");
            }

            // Add logic here to handle what happens when the item is picked up
            // For example, increase the score, update inventory, etc.

            // Destroy the pickup item
            Destroy(currentPickup.gameObject);
            currentPickup = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            currentPickup = other;
            isInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup") && other == currentPickup)
        {
            isInRange = false;
            currentPickup = null;
        }
    }
}
