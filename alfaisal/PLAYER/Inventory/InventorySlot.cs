using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem item;
    public Image icon;
    private Inventory parentInventory; // Reference to the parent inventory
    private PlayerMovement playerMovement; // Reference to PlayerMovement

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); // Initialize PlayerMovement reference
    }

    public void AddItem(InventoryItem newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnItemClicked()
    {
        if (item != null && parentInventory != null)
        {
            parentInventory.ShowItemActions(this);
        }
    }

    public void SetParentInventory(Inventory inventory)
    {
        parentInventory = inventory;
    }

    public void UseItem()
    {
        if (item != null)
        {
            Debug.Log("Using item: " + item.itemName);
            // Implement the item's use functionality
        }
    }

    public void ExamineItem()
    {
        if (item != null)
        {
            Debug.Log("Examining item: " + item.description);
            // Display the item's description
        }
    }

    public void DropItem()
    {
        if (item != null)
        {
            Debug.Log("Dropping item: " + item.itemName);

            // Determine the drop position based on player direction
            Vector3 dropPosition = playerMovement.transform.position;
            float dropOffset = 1f; // Adjust the offset as needed

            // Check if the player is facing left or right
            if (playerMovement.GetComponent<SpriteRenderer>().flipX)
            {
                dropPosition += new Vector3(-dropOffset, 0f, 0f); // Drop to the left of the player
            }
            else
            {
                dropPosition += new Vector3(dropOffset, 0f, 0f); // Drop to the right of the player
            }

            parentInventory.DropItem(item, dropPosition);
        }
    }

    public void CombineItem()
    {
        if (item != null && item.isCombinable)
        {
            Debug.Log("Combining item: " + item.itemName);
            // Implement the combine functionality
        }
    }
}