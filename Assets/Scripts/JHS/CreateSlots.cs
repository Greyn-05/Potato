using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSlots : MonoBehaviour
{
    public GameObject slot;
    public GameObject panel;
    private void Awake()
    {
        CreateSlot();
    }
    private void Start()
    {
        panel.SetActive(false);
    }
    public void CreateSlot()
    {
        for(int i = 0; i < 20; i++)
        {
            Inventory.instance.itemSlotList.Add(Instantiate(slot, gameObject.transform).GetComponent<Slot>());
        }
    }
}
