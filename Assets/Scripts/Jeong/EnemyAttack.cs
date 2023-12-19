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
                AttackToPlayer(); // 공격 실행
            }
        }
    }
    protected virtual void AttackToPlayer()
    {
        animator.SetTrigger("attack"); // 공격 애니메이션 재생
        lastAttackTime = Time.time; // 마지막 공격 시간 갱신
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어에게 데미지 적용 로직
            // 이 부분에서 플레이어의 체력을 감소시키는 로직을 구현합니다.
            Debug.Log("플레이어에게 " + enemyData.atk + "만큼 데미지를 줍니다.");
        }
    }
}
