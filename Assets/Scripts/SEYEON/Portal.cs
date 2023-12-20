using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int portalIndex;
    private bool isActive;

    public void Update()
    {
        CheckPortalActive();

    }
   

    public void CheckPortalActive()
    {
        isActive = GameManager.Instance.EnemyAllDeath();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            Debug.Log("good");
            GameManager.Instance.EnterPortal(portalIndex);
            GameManager.Instance.potalCount = 0;
        }
    }


    //private bool CheckAllMonsterDead()
    //{
    //    // 몬스터 전부 잡았는지 확인
    //    return false;
    //}
}
