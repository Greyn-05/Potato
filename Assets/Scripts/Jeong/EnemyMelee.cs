using UnityEngine;

public class EnemyMelee : EnemyAttack
{
    protected override void AttackToPlayer()
    {
        // ���� ���� ���� �߰� ���� (�ʿ��� ���)
        base.AttackToPlayer(); // �θ� Ŭ������ AttackToPlayer() ȣ��
    }   
}