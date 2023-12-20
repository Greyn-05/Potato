using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public EnemyDataSO _enemyHealth; // ���� ü��
    public EnemySpawn enemySpawn;
    public UIController uIController;
    public GameObject HPPotionItemPrefab; // HPPotionItem �����տ� ���� ���� ����
    public bool choiceDropHPPotion; // HPPotion�� �ش� Enemy���� Drop ��ų���� üũ

    

    private void Awake()
    {
        _enemyHealth.hp = _enemyHealth.maxHealth; // �ʱ� ü�� ����
    }

    public void TakeDamage(float damage)
    {
        _enemyHealth.hp -= damage; // ��������ŭ ü�� ����
        Debug.Log(_enemyHealth.hp);
        if (_enemyHealth.hp <= 0f)
        {
            Die(); // ü���� 0 ������ ��� ��� ó��
        }
    }

    private void Die()
    {
        // ���� ������Ʈ �ı�
        Destroy(gameObject);
        GameManager.Instance.OnEnemyDead();
        if (choiceDropHPPotion == true)
        {
            // 50% Ȯ���� üũ
            if (Random.value < 1f)
            {
                // ���� ���� ������Ʈ ��ġ���� Y������ 1 ���� ���� HPPotionItem ������ �ν��Ͻ�ȭ
                GameObject instantiatedItem = Instantiate(HPPotionItemPrefab, transform.position + Vector3.up * 1f, transform.rotation);

                // �ν��Ͻ�ȭ�� �����ۿ� BoxCollider2D ������Ʈ�� Is Trigger ������ false�� ����
                BoxCollider2D collider = instantiatedItem.GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.isTrigger = false;
                }

                // �ʿ��� ��� Rigidbody2D ������Ʈ �߰� �� ����
                Rigidbody2D rb = instantiatedItem.AddComponent<Rigidbody2D>();
                // ��: rb.gravityScale = 1;
            }
        }
        if (enemySpawn.CurrentScenario == 4)
        {
            uIController.GameClear();
        }
    }
}
