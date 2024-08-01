using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemPickup itemPickup = other.GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            inventory.Add(itemPickup.item);
            Destroy(other.gameObject);
        }
    }
}
