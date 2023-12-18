using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData_", menuName = "Data/EnemyMelee", order = 1)]
public class EnemyDataSO : ScriptableObject
{
    public float hp;
    public float maxHp;
    public float damage;
    public float defence;
    public float attackSpeed;
    public float detectionRange; // �÷��̾� ���� ����
    public float moveSpeed; // �̵� �ӵ�
    public float jumpForce; // ���� ��
    public float jumpCooldown; // ���� ��ٿ� �ð�

}
