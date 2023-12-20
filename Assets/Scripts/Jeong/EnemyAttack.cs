using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyDataSO enemyData; // �� ������ ScriptableObject
    private Transform player; // �÷��̾� Transform
    public float attackRange; // ���� ����
    private Animator animator; // Animator ������Ʈ
    private float lastAttackTime; // ������ ���� �ð� ���

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾� Transform ã��
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position); // �÷��̾���� �Ÿ� ���

            if (distanceToPlayer < attackRange && Time.time > lastAttackTime + (1f / enemyData.attackSpeed))
            {
                AttackToPlayer(); // ���� ����
            }
        }
    }

    protected virtual void AttackToPlayer()
    {
        animator.SetTrigger("attack"); // ���� �ִϸ��̼� ���
        lastAttackTime = Time.time; // ������ ���� �ð� ������Ʈ
    }

    // �ִϸ��̼� �̺�Ʈ�� ���� ȣ��� �޼ҵ�
    public void ApplyDamage()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) < attackRange)
        {
            HealthSystem playerHealthSystem = player.GetComponent<HealthSystem>();
            if (playerHealthSystem != null)
            {
                playerHealthSystem.ChangeHealth(-enemyData.atk);
            }
        }
    }
}
