using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SelectChar : MonoBehaviour
{
    public Character character;
    Animator anim;
    public SelectChar[] chars;


    void Start()
    {
        anim = GetComponent<Animator>();
        if (DataMgr.instance.currentCharacter == character) Onselct();
        else OnDeselct();
    }

    private void OnMouseUpAsButton()
    {
        DataMgr.instance.currentCharacter = character;
        Onselct();
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] != this) chars[i].OnDeselct();
        }
    }
    private void OnDeselct()
    {
        anim.SetBool("Select", false);
        Debug.Log("선택해제");
    }
    private void Onselct()
    {
        anim.SetBool("Select", true);
    }
}
