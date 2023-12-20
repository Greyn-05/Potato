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
        Debug.Log(statsModifiers.Count());
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
        CurrentStatus = new CharacterStatus();
        UpdateStatus((a, b) => b, baseStatus);
        if (CurrentStatus.commonStatus == null)
        {
            CurrentStatus.commonStatus = baseStatus.commonStatus;
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
        UpdateBaseStatus(operation, newModifier.commonStatus);

        if (CurrentStatus.commonStatus == null || newModifier.commonStatus == null) return;
        if (CurrentStatus.commonStatus.GetType() != newModifier.commonStatus.GetType()) return;


        switch (CurrentStatus.commonStatus)
        {
            case EnemyDataSO _:
                EnemyStatus(operation, newModifier);
                break;
        }

    }



    private void UpdateBaseStatus(Func<float, float, float> operation, CommonStatus newStatus)
    {
        if (newStatus == null)
        {
            return;
        }
        CurrentStatus.maxHealth = operation(CurrentStatus.maxHealth, newStatus.maxHealth);
        CurrentStatus.hp = operation(CurrentStatus.hp, newStatus.hp);
        CurrentStatus.atk = operation(CurrentStatus.atk, newStatus.atk);
        CurrentStatus.def = operation(CurrentStatus.def, newStatus.def);
        CurrentStatus.moveSpeed = operation(CurrentStatus.moveSpeed, newStatus.moveSpeed);
        CurrentStatus.attackSpeed = operation(CurrentStatus.attackSpeed, newStatus.attackSpeed);
        CurrentStatus.jumpPower = operation(CurrentStatus.jumpPower, newStatus.jumpPower);
        CurrentStatus.jumpCooldown = operation(CurrentStatus.jumpCooldown, newStatus.jumpCooldown);
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
