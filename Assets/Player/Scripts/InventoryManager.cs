using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity;

}

public class InventoryManager : MonoBehaviour
{
    public HashSet<InventoryItem> inventory = new HashSet<InventoryItem>();

  
    public void AddItem(string itemName)
    {
        InventoryItem existingItem = inventory.FirstOrDefault(item => item.itemName == itemName);

        if (existingItem != null)
        {
            existingItem.quantity++;
        }
        else
        {
            InventoryItem newItem = new InventoryItem { itemName = itemName, quantity = 1 };
            inventory.Add(newItem);
        }
    }

    public bool HasItem(string itemName)
    {
        InventoryItem existingItem = inventory.FirstOrDefault(item => item.itemName == itemName);
        return existingItem != null && existingItem.quantity > 0;
    }

   
}
