using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyDataSO enemyData; // 적 데이터 ScriptableObject
    private Transform player; // 플레이어 Transform
    public float attackRange; // 공격 범위
    private Animator animator; // Animator 컴포넌트
    private float lastAttackTime; // 마지막 공격 시간 기록

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어 Transform 찾기
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position); // 플레이어와의 거리 계산

            if (distanceToPlayer < attackRange && Time.time > lastAttackTime + (1f / enemyData.attackSpeed))
            {
                AttackToPlayer(); // 공격 실행
            }
        }
    }

    protected virtual void AttackToPlayer()
    {
        animator.SetTrigger("attack"); // 공격 애니메이션 재생
        lastAttackTime = Time.time; // 마지막 공격 시간 업데이트
    }

    // 애니메이션 이벤트에 의해 호출될 메소드
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
