using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<InventoryItem> items = new List<InventoryItem>();
    public Transform itemsParent;
    public GameObject inventoryUI;
    public GameObject itemActionPanel; // Reference to the item action panel
    private InventorySlot[] slots;
    private InventorySlot selectedSlot;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optionally make the inventory persist across scenes
        }
    }

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventoryUI.SetActive(false); // Initially hide the inventory UI
        itemActionPanel.SetActive(false); // Initially hide the item action panel
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // Change KeyCode as needed
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf); // Toggle inventory UI
        }
    }

    public void Add(InventoryItem item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(item);
            UpdateUI();
        }
    }

    public void Remove(InventoryItem item)
    {
        items.Remove(item);
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].AddItem(items[i]);
                slots[i].SetParentInventory(this); // Set the parent inventory
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void ShowItemActions(InventorySlot slot)
    {
        selectedSlot = slot;
        itemActionPanel.SetActive(true);
        // Position the itemActionPanel next to the selected slot
        itemActionPanel.transform.position = slot.transform.position;
    }

    public void HideItemActions()
    {
        itemActionPanel.SetActive(false);
        selectedSlot = null;
    }

    public void UseItem()
    {
        if (selectedSlot != null)
        {
            selectedSlot.UseItem();
            HideItemActions();
        }
    }

    public void ExamineItem()
    {
        if (selectedSlot != null)
        {
            selectedSlot.ExamineItem();
            HideItemActions();
        }
    }

    public void DropItem(InventoryItem item, Vector3 dropPosition)
    {
        if (item != null && item.prefab != null)
        {
            GameObject droppedItem = Instantiate(item.prefab, dropPosition, Quaternion.identity);

            Rigidbody2D rb = droppedItem.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false; // Allow physics interactions
            }

            Remove(item);
            HideItemActions();
        }
    }

    public void CombineItem()
    {
        if (selectedSlot != null)
        {
            selectedSlot.CombineItem();
            HideItemActions();
        }
    }

    public void CancelAction()
    {
        HideItemActions();
    }
}