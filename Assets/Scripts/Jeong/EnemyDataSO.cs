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
    public float detectionRange; // 플레이어 감지 범위
    public float moveSpeed; // 이동 속도
    public float jumpForce; // 점프 힘
    public float jumpCooldown; // 점프 쿨다운 시간

}
