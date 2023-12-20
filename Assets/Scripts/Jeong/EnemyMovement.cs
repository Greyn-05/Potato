using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public EnemyDataSO enemyStats; // EnemyStatsSO ScriptableObject ����
    private Rigidbody2D _rigidBody2D;
    private Transform target;
    private float lastJumpTime;
    private Animator animator; // �ִϸ����� ������Ʈ
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ
    

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        lastJumpTime = Time.time - enemyStats.jumpCooldown; // �ʱ�ȭ
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ ������Ʈ ��������
    }

    private void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget < enemyStats.detectionRange)
            {
                MoveTowardsTarget();
                animator.SetBool("isMoving", true); // ������ ���� �� isMoving�� true�� ����

                if (target.position.y > transform.position.y + 1 && Time.time > lastJumpTime + enemyStats.jumpCooldown)
                {
                    Jump();
                }
            }
            else
            {
                animator.SetBool("isMoving", false); // �������� ���� �� isMoving�� false�� ����
            }

            // �÷��̾ ���� ����
            FlipSpriteDirection(target.position.x > transform.position.x);
        }
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        _rigidBody2D.velocity = new Vector2(direction.x * enemyStats.moveSpeed, _rigidBody2D.velocity.y);
    }

    private void Jump()
    {
        _rigidBody2D.AddForce(Vector2.up * enemyStats.jumpPower, ForceMode2D.Impulse);
        lastJumpTime = Time.time;
    }

    private void FlipSpriteDirection(bool isFacingRight)
    {
        Vector3 currentScale = transform.localScale;

        if (isFacingRight)
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // �������� ����
        else
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // ������ ����
    }


}
