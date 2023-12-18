using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyDataSO enemyStats; // EnemyStatsSO ScriptableObject 참조
    private Rigidbody2D _rigidBody2D;
    private Transform target;
    private float lastJumpTime;
    private Animator animator; // 애니메이터 컴포넌트
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        lastJumpTime = Time.time - enemyStats.jumpCooldown; // 초기화
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 컴포넌트 가져오기
    }

    void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget < enemyStats.detectionRange)
            {
                MoveTowardsTarget();
                animator.SetBool("isMoving", true); // 움직임 시작 시 isMoving을 true로 설정

                if (target.position.y > transform.position.y + 1 && Time.time > lastJumpTime + enemyStats.jumpCooldown)
                {
                    Jump();
                }
            }
            else
            {
                animator.SetBool("isMoving", false); // 움직임이 없을 때 isMoving을 false로 설정
            }

            // 플레이어를 향해 반전
            FlipSpriteDirection(target.position.x > transform.position.x);
        }
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        _rigidBody2D.velocity = new Vector2(direction.x * enemyStats.moveSpeed, _rigidBody2D.velocity.y);
    }

    void Jump()
    {
        _rigidBody2D.AddForce(Vector2.up * enemyStats.jumpForce, ForceMode2D.Impulse);
        lastJumpTime = Time.time;
    }

    void FlipSpriteDirection(bool isFacingRight)
    {
        if (isFacingRight)
            transform.localScale = new Vector3(-1, 1, 1); // 왼쪽을 향함
        else
            transform.localScale = new Vector3(1, 1, 1); // 오른쪽을 향함
    }

}
