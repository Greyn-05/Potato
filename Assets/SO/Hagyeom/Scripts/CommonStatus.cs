using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;


[CreateAssetMenu(fileName = "CommonStatus", 
    menuName = "Status/CommonStatus", order = 0)]
public class CommonStatus : ScriptableObject
{
    [Header("Status")]
    public float maxHealth;
    public float hp;
    public float atk;
    public float def;
    public float moveSpeed;
    public float attackSpeed;
    public float jumpPower;
    public float jumpCooldown;
    public float exp;
    public LayerMask target;


}
