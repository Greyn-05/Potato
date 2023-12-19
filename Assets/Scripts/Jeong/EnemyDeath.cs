using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public EnemyDataSO _enemyHealth; // 적의 체력

    private void Awake()
    {
        _enemyHealth.hp = _enemyHealth.maxHealth; // 초기 체력 설정
    }

    public void TakeDamage(float damage)
    {
        _enemyHealth.hp -= damage; // 데미지만큼 체력 감소
        Debug.Log(_enemyHealth.hp);
        if (_enemyHealth.hp <= 0f)
        {
            Die(); // 체력이 0 이하일 경우 사망 처리
        }
    }

    private void Die()
    {
        // 적 사망시 처리 로직 (예: 애니메이션 재생, 객체 파괴 등)
        Destroy(gameObject); // 적 게임 오브젝트 파괴
    }
}
