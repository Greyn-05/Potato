using UnityEngine;

public class EnemyMelee : EnemyAttack
{
    protected override void AttackToPlayer()
    {
        // 근접 공격 관련 추가 로직 (필요한 경우)
        base.AttackToPlayer(); // 부모 클래스의 AttackToPlayer() 호출
    }   
}