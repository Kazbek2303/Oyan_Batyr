using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public LayerMask interactableLayer;

    // Create a list to store interactable objects
    private List<IInteractable> inventory = new List<IInteractable>();


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
         
            CheckInteracte();
        }
    }

    private void CheckInteracte()
    {
        // Start a new interaction
    
        Collider[] colliders = Physics.OverlapSphere(InteractorSource.position, InteractRange, interactableLayer);
        foreach (Collider collider in colliders)
        {
          
            if (collider.TryGetComponent<IInteractable>(out var interactObj))
            {
               
                // Check if the interactable object is within the specified range
                float distance = Vector3.Distance(InteractorSource.position, collider.transform.position);
                if (distance <= InteractRange &&  !inventory.Contains(interactObj))
                {
                    
                    interactObj.Interact();  
                    inventory.Add(interactObj);
                }
            }
        }
    }


}
