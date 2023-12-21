using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RealStart : MonoBehaviour
{
    public void Startbt()
    {
        if (DataMgr.instance != null && DataMgr.instance.currentCharacter != Character.Null)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            Debug.Log("캐릭터를 골라주세요.");
        }
    }
}
