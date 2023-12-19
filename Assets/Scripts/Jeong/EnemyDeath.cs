using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public EnemyDataSO _enemyHealth; // ���� ü��

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
        // �� ����� ó�� ���� (��: �ִϸ��̼� ���, ��ü �ı� ��)
        Destroy(gameObject); // �� ���� ������Ʈ �ı�
    }
}
