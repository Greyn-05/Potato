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
                AttackToPlayer(); // ���� ����
            }
        }
    }
    protected virtual void AttackToPlayer()
    {
        animator.SetTrigger("attack"); // ���� �ִϸ��̼� ���
        lastAttackTime = Time.time; // ������ ���� �ð� ����
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� ������ ���� ����
            // �� �κп��� �÷��̾��� ü���� ���ҽ�Ű�� ������ �����մϴ�.
            Debug.Log("�÷��̾�� " + enemyData.atk + "��ŭ �������� �ݴϴ�.");
        }
    }
}
