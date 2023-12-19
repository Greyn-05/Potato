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
    CommonStatus commonStatusInstance; // CommonStatus 인스턴스를 클래스 레벨 변수로 선언

    private void Awake()
    {
        mytext = GetComponent<Text>();
        myslider = GetComponent<Slider>();
        commonStatusInstance = ScriptableObject.CreateInstance<CommonStatus>(); // CommonStatus 인스턴스 생성 및 할당
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