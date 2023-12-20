using UnityEngine;

public class EnemyShooter : EnemyAttack
{
    public GameObject bulletPrefab; // 총알 프리팹 참조
    public Transform bulletSpawnPoint; // 총알이 생성될 위치

    protected override void AttackToPlayer()
    {
        base.AttackToPlayer(); // 기본 공격 로직 실행

        // 총알 생성
        InstantiateBullet();
    }

    void InstantiateBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
        else
        {
            Debug.LogError("총알 프리팹 또는 생성 위치가 설정되지 않았습니다.");
        }
    }
}
