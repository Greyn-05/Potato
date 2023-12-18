using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, HP, Mana}
    public InfoType type;

    Text mytext;
    Slider myslider;

    private void Awake()
    {
        mytext = GetComponent<Text>();
        myslider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type) 
        {
            case InfoType.Exp:       //���� ���ӸŴ����� ����� �ּ��� Ǯ� ���
                float maxExp = 100;  //GameManager.instance.����ġ;
                float curExp = 100;  //GameManager.instance.�������������� ����ġ;
                myslider.value = curExp / maxExp;
                break;
            case InfoType.HP:
                float maxhp = 100;   //GameManager.instance.~~
                float curhp = 50;   //GameManager.instance.~~
                myslider.value = curhp / maxhp;
                break;
            case InfoType.Mana:
                float maxmp = 100;
                float curmp = 50;
                myslider.value = curmp / maxmp;
                break;
        }
    }
}
