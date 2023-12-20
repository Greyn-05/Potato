using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int TrapDamage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어 태그와 충돌했는지 확인
        {
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            healthSystem.ChangeHealth(-TrapDamage);
        }
        else if (other.CompareTag("Enemy"))
        {
            //적에게도 데미지? 지워도 ㅇㅇ 가능ㄴㅇ
        }
    }
}
