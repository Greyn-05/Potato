using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyDataSO enemyData; // 적 데이터 ScriptableObject
    private Transform player; // 플레이어 Transform
    public float attackRange; // 공격 범위
    private Animator animator; // 애니메이터 컴포넌트
    private float lastAttackTime; // 마지막 공격 시간 기록

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어 Transform 찾기
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position); // 플레이어와의 거리 계산

            // 플레이어가 공격 범위 안에 있고, 충분한 쿨다운 시간이 지났는지 확인
            if (distanceToPlayer < attackRange && Time.time > lastAttackTime + (1f / enemyData.attackSpeed))
            {
                AttackPlayer(); // 공격 실행
            }
        }
    }

    void AttackPlayer()
    {
        animator.SetTrigger("attack"); // 'attack' 트리거로 공격 애니메이션 재생
        lastAttackTime = Time.time; // 마지막 공격 시간 갱신

        // 추가적인 공격 처리 로직 구현 (예: 플레이어 체력 감소)
        Debug.Log("플레이어에게 " + enemyData.damage + "만큼 데미지를 줍니다.");
    }
}
