using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { HP,Gold }
    public InfoType type;

    Text mytext;
    Slider myslider;
    CommonStatus commonStatusInstance; // CommonStatus �ν��Ͻ��� Ŭ���� ���� ������ ����

    private void Awake()
    {
        mytext = GetComponent<Text>();
        myslider = GetComponent<Slider>();
        commonStatusInstance = ScriptableObject.CreateInstance<CommonStatus>(); // CommonStatus �ν��Ͻ� ���� �� �Ҵ�
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.HP:
                float maxhp = commonStatusInstance.maxHealth;
                float curhp = commonStatusInstance.hp;
                myslider.value = curhp / maxhp;
                break;
            case InfoType.Gold:
                float curgold = 100;
                mytext.text = curgold.ToString();
                break;
        }
    }
}