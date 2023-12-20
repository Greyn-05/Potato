using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int portalIndex;
    private bool isActive;

    private void Start()
    {
        CheckPortalActive();
    }
   

    public void CheckPortalActive()
    {
        isActive = CheckAllMonsterDead();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            GameManager.Instance.EnterPortal(portalIndex);
        }
    }


    private bool CheckAllMonsterDead()
    {
        // ���� ���� ��Ҵ��� Ȯ��
        return false;
    }
}
