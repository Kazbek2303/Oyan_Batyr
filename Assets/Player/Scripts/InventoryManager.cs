using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity;
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        InventoryItem other = (InventoryItem)obj;
        return itemName == other.itemName;
    }

    public override int GetHashCode()
    {
        return itemName != null ? itemName.GetHashCode() : 0;
    }
}

public class InventoryManager : MonoBehaviour
{
    public HashSet<InventoryItem> inventory = new HashSet<InventoryItem>();
    public int itemsAddedCount = 0;

    public void AddItem(string itemName)
    {
        InventoryItem existingItem = inventory.FirstOrDefault(item => item.itemName == itemName);

        if (existingItem != null)
        {
            existingItem.quantity++;
            Debug.Log("quantity++");
        }
        else
        {
            InventoryItem newItem = new InventoryItem { itemName = itemName, quantity = 1 };
            inventory.Add(newItem);
            itemsAddedCount++;
            Debug.Log("Added new item: " + itemName);

            if (itemsAddedCount == 3)
            {
                //LoadNewScene();
            }
        }
    }

    public bool HasItem(string itemName)
    {
        InventoryItem existingItem = inventory.FirstOrDefault(item => item.itemName == itemName);
        return existingItem != null && existingItem.quantity > 0;
    }
    public bool HasAllItems(List<string> itemNames)
    {
        foreach (var itemName in itemNames)
        {
            if (!HasItem(itemName))
            {
                return false;
            }
        }
        return true;
    }
    public void LoadNewScene()
    {
        SceneManager.LoadScene("SwordAnim");
    }
}
