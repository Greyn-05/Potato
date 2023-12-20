using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { HpPotion, Weapon, Armor, JumpPotion }
public class Item : MonoBehaviour
{
    [Header("ItemInformation")]
    public ItemType type;
    public int itemPotionRecoveryAmount;

    [SerializeField]
    private List<CharacterStatus> statsModifier;
    [SerializeField]
    private CharacterStatus itemsStats;
    PlayerItem playeritem;
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
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            characterStatusHandler = collision.gameObject.GetComponent<CharacterStatusHandler>();
            playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            playeritem = collision.gameObject.GetComponent<PlayerItem>();
            ClassifyItem(type , collision);
            Destroy(gameObject);
        }
    }
  /*  public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            characterStatusHandler = collision.GetComponent<CharacterStatusHandler>();
            playerHealth = collision.GetComponent<HealthSystem>();
            playeritem = collision.GetComponent<PlayerItem>();
            ClassifyItem(type);
            Destroy(gameObject);
        }
    }*/
    private void ClassifyItem(ItemType itemtype, Collision2D collision)
    {
        switch (itemtype)
        {
            case ItemType.HpPotion: // 플레이어의 회복할때
                playerHealth.ChangeHealth(itemPotionRecoveryAmount);
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
            case ItemType.JumpPotion:
                characterStatusHandler.AddStatModifier(itemsStats);
                //playeritem.status = itemsStats;
                // playeritem.potionTime = true;
                //playeritem.time = 3f;
                collision.gameObject.GetComponent<PlayerItem>().status = itemsStats;
                collision.gameObject.GetComponent<PlayerItem>().time = 3f;
                collision.gameObject.GetComponent<PlayerItem>().potionTime = true;
                break;
        }
    }
}
