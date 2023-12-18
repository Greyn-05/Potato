using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyDataSO enemyData; // �� ������ ScriptableObject
    private Transform player; // �÷��̾� Transform
    public float attackRange; // ���� ����
    private Animator animator; // �ִϸ����� ������Ʈ
    private float lastAttackTime; // ������ ���� �ð� ���

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾� Transform ã��
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position); // �÷��̾���� �Ÿ� ���

            // �÷��̾ ���� ���� �ȿ� �ְ�, ����� ��ٿ� �ð��� �������� Ȯ��
            if (distanceToPlayer < attackRange && Time.time > lastAttackTime + (1f / enemyData.attackSpeed))
            {
                AttackPlayer(); // ���� ����
            }
        }
    }

    void AttackPlayer()
    {
        animator.SetTrigger("attack"); // 'attack' Ʈ���ŷ� ���� �ִϸ��̼� ���
        lastAttackTime = Time.time; // ������ ���� �ð� ����

        // �߰����� ���� ó�� ���� ���� (��: �÷��̾� ü�� ����)
        Debug.Log("�÷��̾�� " + enemyData.damage + "��ŭ �������� �ݴϴ�.");
    }
}
