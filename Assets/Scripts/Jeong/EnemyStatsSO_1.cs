using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData_", menuName = "Data/EnemyMelee", order = 1)]
public class EnemyStatsSO_1 : ScriptableObject
{
    public int HP;
    public int MaxHp;
    public int Damage;
    public int Defence;
    public int AttackSpeed;
    public int MoveSpeed;

}
