using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { HP, Gold, Atk, Def, AS }
    public InfoType type;

    Text mytext;
    Slider myslider;
    CharacterStatus commonStatusInstance;
    HealthSystem healthSystemInstance;

    private void Awake()
    {
        mytext = GetComponent<Text>();
        myslider = GetComponent<Slider>();
        commonStatusInstance = FindObjectOfType<CharacterStatusHandler>().CurrentStatus;
        healthSystemInstance = FindObjectOfType<HealthSystem>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.HP:
                float maxhp = healthSystemInstance.MaxHealth;
                float curhp = healthSystemInstance.CurrentHealth;
                myslider.value = curhp / maxhp;
                break;
            case InfoType.Atk:
                float atk = commonStatusInstance.atk;
                mytext.text = atk.ToString();
                break;
            case InfoType.AS:
                float atks = commonStatusInstance.attackSpeed;
                mytext.text = atks.ToString();
                break;
        }
    }
}
