using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { HpPotion, Weapon, Armor }
public class Item : MonoBehaviour
{
    [Header("ItemInformation")]
    public ItemType type;
    public int itemPotionRecoveryAmount;

    [SerializeField]
    private List<CharacterStatus> statsModifier;
    CharacterStatusHandler characterStatusHandler;
    HealthSystem playerHealth;
    public GameObject itemImage;
    private void Awake()
    {
        
    }
    void Start()
    {

    }
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "testplayer")
        {
            characterStatusHandler = collision.GetComponent<CharacterStatusHandler>();
            playerHealth = collision.GetComponent<HealthSystem>();
            ClassifyItem(type);
            Destroy(gameObject);
        }
    }
    private void ClassifyItem(ItemType itemtype)
    {
        switch (itemtype)
        {
            
            case ItemType.HpPotion: // 플레이어의 회복할때
                break;
            case ItemType.Armor:
                for (int i = 0; i < Inventory.instance.itemSlotList.Count; i++)
                {
                    if (Inventory.instance.itemSlotList[i].transform.childCount == 0)
                    {
                        Instantiate(itemImage, Inventory.instance.itemSlotList[i].transform).GetComponent<DraggableUI>().statusHandler= characterStatusHandler;
                        break;
                    }
                }
                break;
            case ItemType.Weapon:
                for (int i = 0; i < Inventory.instance.itemSlotList.Count; i++)
                {
                    if (Inventory.instance.itemSlotList[i].transform.childCount == 0)
                    {
                        Instantiate(itemImage, Inventory.instance.itemSlotList[i].transform).GetComponent<DraggableUI>().statusHandler = characterStatusHandler;
                        break;
                    }
                }
                break;
        }
    }
}
