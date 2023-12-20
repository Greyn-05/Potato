using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMapCreator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.nextMapCreator == true)
        {
            GameManager.Instance.nextMapCreator = false;
            Destroy(gameObject);
        }
    }
}
