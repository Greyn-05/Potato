using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Field
    private CharacterController _controller;
    private CharacterStatusHandler _status;

    [SerializeField] private Transform _pos;
    [SerializeField] private Vector2 _boxSize;
    #endregion


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _status = GetComponent<CharacterStatusHandler>();
    }


    private void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_pos.position, _boxSize, 0);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy")) // 콜라이더가 적인지 확인합니다.
            {
                EnemyDeath enemyDeath = collider.GetComponent<EnemyDeath>();
                if (enemyDeath != null)
                {
                    float damage = _status.CurrentStatus.atk; // 플레이어의 공격력을 가져옵니다.
                    //enemyDeath.TakeDamage(damage); // 적에게 데미지를 적용합니다.
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_pos.position, _boxSize);
    }
}
