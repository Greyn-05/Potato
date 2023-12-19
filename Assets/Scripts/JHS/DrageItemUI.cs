using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform canvas;             
    private Transform previousParent;     
    private RectTransform rect;          
    private CanvasGroup canvasGroup;

    public GameObject descriptionPanel;
    public GameObject equipUI;

    [SerializeField]
    private List<CharacterStatus> statsModifier;
    public CharacterStatusHandler statusHandler;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        EquipItem();
    }
    public void EquipItem()
    {
        if (descriptionPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.GetComponent<Item>().type == ItemType.Weapon)
            {
                if (!equipUI.activeSelf&&Inventory.instance.equipWeaponCount==0)
                {
                    equipUI.SetActive(true);
                    Inventory.instance.equipWeaponCount++;
                    //���� ���� �Ҷ�
                    foreach(CharacterStatus stat in statsModifier)
                    {
                        statusHandler.AddStatModifier(stat);
                    }
                }
                else if (equipUI.activeSelf && Inventory.instance.equipWeaponCount == 1)
                {
                    equipUI.SetActive(false);
                    Inventory.instance.equipWeaponCount--;
                    //���� �������� �Ҷ�
                    foreach (CharacterStatus stat in statsModifier)
                    {
                        statusHandler.RemoveStatModifier(stat);
                    }
                }
            }
            else if(gameObject.GetComponent<Item>().type == ItemType.Armor)
            {
                if (!equipUI.activeSelf&& Inventory.instance.equipArmorCount == 0)
                {
                    equipUI.SetActive(true);
                    Inventory.instance.equipArmorCount++;
                    //�� ���� �Ҷ�
                    foreach (CharacterStatus stat in statsModifier)
                    {
                        statusHandler.AddStatModifier(stat);
                    }
                }
                else if (equipUI.activeSelf && Inventory.instance.equipArmorCount ==1)
                {
                    equipUI.SetActive(false);
                    Inventory.instance.equipArmorCount--;
                    //�� �������� �Ҷ�
                    foreach (CharacterStatus stat in statsModifier)
                    {
                        statusHandler.RemoveStatModifier(stat);
                    }
                }
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent;

        transform.SetParent(canvas);        
        transform.SetAsLastSibling();    

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas)
        {
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
        }

        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionPanel.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
    }

}

