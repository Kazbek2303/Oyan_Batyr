using System.Collections.Generic;
using UnityEngine;

public class SwordCheckTrigger : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public List<string> requiredItems;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (inventoryManager.HasAllItems(requiredItems))
            {
                Debug.Log("All sword parts collected!");
                // Additional logic for when all items are collected
            }
            else
            {
                Debug.Log("You are missing some sword parts!");
                // Additional logic for when not all items are collected
            }
        }
    }
}
