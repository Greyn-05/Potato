using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;

    private float maxhp = 100;
    private float curhp = 100;
    void Start()
    {
        hpbar.value = (float) curhp / (float) maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if(curhp > 0)
            {
                curhp -= 10;
            }
            else
            {
                curhp = 0;
            }
        }
        Checkhp();
    }

    private void Checkhp() 
    {
        hpbar.value = Mathf.Lerp(hpbar.value, hpbar.value = (float)curhp / (float)maxhp, Time.deltaTime * 10);
    }
}
