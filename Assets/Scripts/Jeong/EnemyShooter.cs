using UnityEngine;

public class EnemyShooter : EnemyAttack
{
    public GameObject bulletPrefab; // �Ѿ� ������ ����
    public Transform bulletSpawnPoint; // �Ѿ��� ������ ��ġ

    protected override void AttackToPlayer()
    {
        base.AttackToPlayer(); // �⺻ ���� ���� ����

        // �Ѿ� ����
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
            Debug.LogError("�Ѿ� ������ �Ǵ� ���� ��ġ�� �������� �ʾҽ��ϴ�.");
        }
    }
}
