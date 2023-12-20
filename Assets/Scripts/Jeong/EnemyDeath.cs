using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public EnemyDataSO _enemyHealth; // 적의 체력
    public GameObject HPPotionItemPrefab; // HPPotionItem 프리팹에 대한 공개 참조
    public bool choiceDropHPPotion; // HPPotion을 해당 Enemy에게 Drop 시킬건지 체크

    

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
        // 게임 오브젝트 파괴
        Destroy(gameObject);
        if (choiceDropHPPotion == true)
        {
            // 50% 확률로 체크
            if (Random.value < 1f)
            {
                // 현재 게임 오브젝트 위치에서 Y축으로 1 단위 위에 HPPotionItem 프리팹 인스턴스화
                GameObject instantiatedItem = Instantiate(HPPotionItemPrefab, transform.position + Vector3.up * 1f, transform.rotation);

                // 인스턴스화된 아이템에 BoxCollider2D 컴포넌트의 Is Trigger 설정을 false로 변경
                BoxCollider2D collider = instantiatedItem.GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.isTrigger = false;
                }

                // 필요한 경우 Rigidbody2D 컴포넌트 추가 및 설정
                Rigidbody2D rb = instantiatedItem.AddComponent<Rigidbody2D>();
                // 예: rb.gravityScale = 1;
            }
        }
        GameManager.Instance.potalCount++;
    }

    
}
