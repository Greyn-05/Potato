using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private HealthSystem _healthSystem;

    private void Awake()
    {
        // HealthSystem ������Ʈ�� �����ɴϴ�.
        _healthSystem = GetComponent<HealthSystem>();
        if (_healthSystem != null)
        {
            // OnDeath �̺�Ʈ�� HandleDeath �޼ҵ带 �����մϴ�.
            _healthSystem.OnDeath += HandleDeath;
        }
    }

    private void OnDestroy()
    {
        if (_healthSystem != null)
        {
            // �޸� ������ �����ϱ� ���� �̺�Ʈ ������ �����մϴ�.
            _healthSystem.OnDeath -= HandleDeath;
        }
    }

    private void HandleDeath()
    {
        // �� ����� ó���� ������ ���⿡ �ۼ��մϴ�.
        // ��: ��� �ִϸ��̼� ���, ��ƼŬ ȿ�� ��
        Debug.Log("���� ����߽��ϴ�.");

        // ������: �� ���� ������Ʈ�� �ı��մϴ�.
        Destroy(gameObject);
    }
}
