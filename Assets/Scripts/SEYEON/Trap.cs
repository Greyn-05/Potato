using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int TrapDamage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾� �±׿� �浹�ߴ��� Ȯ��
        {
            //�÷��̾�� ������ ���� ����
        }
        else if (other.CompareTag("Enemy"))
        {
            //�����Ե� ������? ������ ���� ���ɤ���
        }
    }
}
