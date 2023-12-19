using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class CharacterStatusHandler : MonoBehaviour
{
    #region Field
    [SerializeField] private CharacterStatus baseStatus;
    public CharacterStatus CurrentStatus { get; private set; }
    public List<CharacterStatus> statsModifiers = new List<CharacterStatus>();
    #endregion

    #region Init
    private void Awake()
    {
        UpdatePlayerStatus();
    }
    #endregion

    #region UpdateStatus
    private void UpdatePlayerStatus()
    {
        // baseStatus �Է�
        CommonStatus commonStatus = null;
        //InitBaseStatus();   // baseStatus�� commonStatus�� �־�α�
        if (baseStatus.commonStatus != null)    // currentStatus�� ����� commonStatus�ޱ�
        {
            commonStatus = Instantiate(baseStatus.commonStatus);
        }

        // currentStatus ���� �Է� 
        CurrentStatus = new CharacterStatus { commonStatus = commonStatus };
        UpdateStats((a, b) => b, baseStatus);
        if(CurrentStatus.commonStatus != null)
        {
            CurrentStatus = baseStatus;
        }

        // characterStatus ����
        foreach (CharacterStatus modifier in statsModifiers.OrderBy(x => x.statusChangeType))
        {
            if (modifier.statusChangeType == StatusChangeType.OVERRIDE) // �����
            {
                UpdateStats((a, b) => b, modifier);
            }
            else if (modifier.statusChangeType == StatusChangeType.ADD) // �߰�
            {
                UpdateStats((a, b) => a + b, modifier);
            }
            else if (modifier.statusChangeType == StatusChangeType.MULTIPLE) // ������
            {
                UpdateStats((a, b) => a * b, modifier);
            }
        }

        LimitAllStatus();

    }

    // baseStauts���� �� �ޱ�
    //private void InitBaseStatus()
    //{
    //    baseStatus.maxHealth = baseStatus.commonStatus.maxHealth;
    //    baseStatus.hp = baseStatus.commonStatus.hp;
    //    baseStatus.atk = baseStatus.commonStatus.atk;
    //    baseStatus.def = baseStatus.commonStatus.def;
    //    baseStatus.moveSpeed = baseStatus.commonStatus.moveSpeed;
    //    baseStatus.attackSpeed  = baseStatus.commonStatus.attackSpeed;
    //    baseStatus.exp = baseStatus.commonStatus.exp;
    //    baseStatus.jumpPower = baseStatus.commonStatus.jumpPower;
    //    baseStatus.jumpCooldown = baseStatus.commonStatus.jumpCooldown;
    //    baseStatus.target = baseStatus.commonStatus.target;
    //}

    // status�� ��ȭ �ޱ�
    private void UpdateStats(Func<float, float, float> operation, CharacterStatus newModifier)
    {
        UpdateBaseStatus(operation, CurrentStatus.commonStatus, newModifier.commonStatus);

        if (CurrentStatus.commonStatus == null || newModifier.commonStatus == null) return;
        if (CurrentStatus.commonStatus.GetType() != newModifier.commonStatus.GetType()) return;


        switch (CurrentStatus.commonStatus)
        {
            case EnemyDataSO _:
                EnemyStatus(operation, newModifier);
                break;
        }

    }

    

    private void UpdateBaseStatus(Func<float, float, float> operation, CommonStatus currentStatus, CommonStatus newStatus)
    {
        if (currentStatus == null || newStatus == null || currentStatus.GetType() != newStatus.GetType())
        {
            return;
        }

        currentStatus.maxHealth = operation(currentStatus.maxHealth, newStatus.maxHealth);
        currentStatus.hp =  operation(currentStatus.hp, newStatus.hp);
        currentStatus.atk = operation(currentStatus.atk, newStatus.atk);
        currentStatus.def = operation(currentStatus.def, newStatus.def);
        currentStatus.moveSpeed = operation(currentStatus.moveSpeed, newStatus.moveSpeed);
        currentStatus.attackSpeed = operation(currentStatus.attackSpeed, newStatus.attackSpeed);
        currentStatus.jumpPower = operation(currentStatus.jumpPower, newStatus.jumpPower);
        currentStatus.jumpCooldown = operation(currentStatus.jumpCooldown, newStatus.jumpCooldown);
    }

    private void EnemyStatus(Func<float, float, float> operation, CharacterStatus newModifier)
    {
        EnemyDataSO currentEnemyStatus = (EnemyDataSO)CurrentStatus.commonStatus;

        if (!(newModifier.commonStatus is EnemyDataSO))
        {
            return;
        }

        EnemyDataSO EnemyStatusModifier = (EnemyDataSO)newModifier.commonStatus;
        currentEnemyStatus.detectionRange = operation(currentEnemyStatus.detectionRange, EnemyStatusModifier.detectionRange);
    }

    #region minValue
    private void LimitStatus(ref float stat, float minVal)
    {
        stat = Mathf.Max(stat, minVal);
    }


    private void LimitAllStatus()
    {
        if (CurrentStatus == null || CurrentStatus.commonStatus == null)
        {
            return;
        }

        LimitStatus(ref CurrentStatus.maxHealth, baseStatus.maxHealth);
        LimitStatus(ref CurrentStatus.atk, baseStatus.atk);
        LimitStatus(ref CurrentStatus.def, baseStatus.def);
        LimitStatus(ref CurrentStatus.moveSpeed, baseStatus.moveSpeed);
        LimitStatus(ref CurrentStatus.attackSpeed, baseStatus.attackSpeed);
    }
    #endregion
    #endregion
}
