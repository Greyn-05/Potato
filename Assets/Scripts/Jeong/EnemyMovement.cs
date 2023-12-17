using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyMovementSO enemyStats; // EnemyStatsSO ScriptableObject 참조
    private Rigidbody2D _rigidBody2D;
    private Transform target;
    private float lastJumpTime;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        lastJumpTime = Time.time - enemyStats.jumpCooldown; // 초기화
    }

    void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget < enemyStats.detectionRange)
            {
                MoveTowardsTarget();

                if (target.position.y > transform.position.y + 1 && Time.time > lastJumpTime + enemyStats.jumpCooldown)
                {
                    Jump();
                }
            }
        }
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        _rigidBody2D.velocity = new Vector2(direction.x * enemyStats.moveSpeed, _rigidBody2D.velocity.y); // 이동
    }

    void Jump()
    {
        _rigidBody2D.AddForce(Vector2.up * enemyStats.jumpForce, ForceMode2D.Impulse); // 점프
        lastJumpTime = Time.time; // 시간 업데이트
    }
}
