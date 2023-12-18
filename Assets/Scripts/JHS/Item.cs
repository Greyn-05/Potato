using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Image itemImage;
    void Start()
    {

    }
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "testplayer")
        {
           for(int i = 0; i < Inventory.instance.itemSlotList.Count; i++)
            {
                if (Inventory.instance.itemSlotList[i].transform.childCount == 0)
                {
                    Instantiate(itemImage, Inventory.instance.itemSlotList[i].transform);
                    break;
                }
            }
        }
    }
}
