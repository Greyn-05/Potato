using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyDataSO enemyData; // �� ������ ScriptableObject
    private Transform player; // �÷��̾� Transform
    public float attackRange; // ���� ����
    private Animator animator; // �ִϸ����� ������Ʈ
    private float lastAttackTime; // ������ ���� �ð� ���

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾� Transform ã��
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position); // �÷��̾���� �Ÿ� ���

            // �÷��̾ ���� ���� �ȿ� �ְ�, ����� ��ٿ� �ð��� �������� Ȯ��
            if (distanceToPlayer < attackRange && Time.time > lastAttackTime + (1f / enemyData.attackSpeed))
            {
                AttackToPlayer(); // ���� ����
            }
        }
    }
    protected virtual void AttackToPlayer()
    {
        animator.SetTrigger("attack"); // ���� �ִϸ��̼� ���
        lastAttackTime = Time.time; // ������ ���� �ð� ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (playerHealthSystem != null)
            {
                playerHealthSystem.ChangeHealth(-enemyData.atk);
            }
        }
    }
}
