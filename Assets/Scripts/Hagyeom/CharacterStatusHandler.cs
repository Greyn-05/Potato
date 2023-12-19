using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class CharacterStatusHandler : MonoBehaviour
{
    #region Field
    [SerializeField] private CharacterStatus baseStatus;
    public CharacterStatus CurrentStatus { get; private set; }
    public List<CharacterStatus> statsModifiers = new List<CharacterStatus>();
    private PlayerItem _playerItem;
    #endregion

    #region Init
    private void Awake()
    {
        UpdatePlayerStatus();
        _playerItem = GetComponent<PlayerItem>();
    }

    private void Start()
    {
        _playerItem.OnPotionEnd += RemoveStatModifier;
    }
    #endregion

    #region Modifier
    public void AddStatModifier(CharacterStatus statusModifier)
    {
        statsModifiers.Add(statusModifier);
        UpdatePlayerStatus();
    }

    public void RemoveStatModifier(CharacterStatus statusModifier)
    {
        statsModifiers.Remove(statusModifier);
        UpdatePlayerStatus();
    }
    #endregion


    #region UpdateStatus
    private void UpdatePlayerStatus()
    {
        // baseStatus 입력
        CommonStatus commonStatus = null;
        if (baseStatus.commonStatus != null)
        {
            commonStatus = Instantiate(baseStatus.commonStatus);
        }

        // currentStatus 기초 입력 
        CurrentStatus = new CharacterStatus { commonStatus = commonStatus };
        UpdateStatus((a, b) => b, baseStatus);
        if(CurrentStatus.commonStatus != null)
        {
            CurrentStatus = baseStatus;
        }

        // characterStatus 수정
        foreach (CharacterStatus modifier in statsModifiers.OrderBy(x => x.statusChangeType))
        {
            if (modifier.statusChangeType == StatusChangeType.OVERRIDE) // 덮어쓰기
            {
                UpdateStatus((a, b) => b, modifier);
            }
            else if (modifier.statusChangeType == StatusChangeType.ADD) // 추가
            {
                UpdateStatus((a, b) => a + b, modifier);
            }
            else if (modifier.statusChangeType == StatusChangeType.MULTIPLE) // 곱연산
            {
                UpdateStatus((a, b) => a * b, modifier);
            }
        }

        SetCurrentStatus();
        LimitAllStatus();

    }

    


    // status값 변화 받기
    private void UpdateStatus(Func<float, float, float> operation, CharacterStatus newModifier)
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


    private void SetCurrentStatus()
    {
        if (CurrentStatus == null || CurrentStatus.commonStatus == null)
        {
            return;
        }

        LimitStatus(ref CurrentStatus.maxHealth, CurrentStatus.commonStatus.maxHealth);
        LimitStatus(ref CurrentStatus.atk, CurrentStatus.commonStatus.atk);
        LimitStatus(ref CurrentStatus.def, CurrentStatus.commonStatus.def);
        LimitStatus(ref CurrentStatus.moveSpeed, CurrentStatus.commonStatus.moveSpeed);
        LimitStatus(ref CurrentStatus.attackSpeed, CurrentStatus.commonStatus.attackSpeed);
        LimitStatus(ref CurrentStatus.jumpPower, CurrentStatus.commonStatus.jumpPower);
        LimitStatus(ref CurrentStatus.jumpCooldown, CurrentStatus.commonStatus.jumpCooldown);
    }

    private void LimitAllStatus()
    {
        if (CurrentStatus == null || CurrentStatus.commonStatus == null)
        {
            return;
        }

        LimitStatus(ref CurrentStatus.maxHealth, baseStatus.commonStatus.maxHealth);
        LimitStatus(ref CurrentStatus.atk, baseStatus.commonStatus.atk);
        LimitStatus(ref CurrentStatus.def, baseStatus.commonStatus.def);
        LimitStatus(ref CurrentStatus.moveSpeed, baseStatus.commonStatus.moveSpeed);
        LimitStatus(ref CurrentStatus.attackSpeed, baseStatus.commonStatus.attackSpeed);
    }
    #endregion
    #endregion
}
