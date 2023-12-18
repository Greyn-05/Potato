using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeongManager : MonoBehaviour
{
    private static JeongManager instance;


    private void Awake()
    {
        instance = this;
    }


   
}
