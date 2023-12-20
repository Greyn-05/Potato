using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public GameObject inventoryPanel;
    public List<Slot> itemSlotList = new List<Slot>();

    public int equipWeaponCount;
    public int equipArmorCount;

    private void Awake()
    {
            instance = this; 
    }
    private void Update()
    {
        OnOffInventoryPanel();
        
    }
    private void OnOffInventoryPanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitInventoryPanel();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(true);
        }
    }
    public void ExitInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }
}
