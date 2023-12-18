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
        // baseStatus 입력
        CommonStatus commonStatus = null;
        InitBaseStatus();   // baseStatus에 commonStatus값 넣어두기
        if (baseStatus.commonStatus != null)    // currentStatus에 쥐어줄 commonStatus받기
        {
            commonStatus = Instantiate(baseStatus.commonStatus);
        }

        // currentStatus 기초 입력 
        CurrentStatus = new CharacterStatus { commonStatus = commonStatus };
        if(CurrentStatus.commonStatus != null)
        {
            CurrentStatus = baseStatus;
        }

        // characterStatus 수정
        foreach (CharacterStatus modifier in statsModifiers.OrderBy(x => x.statusChangeType))
        {
            if (modifier.statusChangeType == StatusChangeType.OVERRIDE) // 덮어쓰기
            {
                UpdateStats((a, b) => b, modifier);
            }
            else if (modifier.statusChangeType == StatusChangeType.ADD) // 추가
            {
                UpdateStats((a, b) => a + b, modifier);
            }
            else if (modifier.statusChangeType == StatusChangeType.MULTIPLE) // 곱연산
            {
                UpdateStats((a, b) => a * b, modifier);
            }
        }

        LimitAllStatus();

    }

    // baseStauts기초 값 받기
    private void InitBaseStatus()
    {
        baseStatus.maxHealth = baseStatus.commonStatus.maxHealth;
        baseStatus.hp = baseStatus.commonStatus.hp;
        baseStatus.atk = baseStatus.commonStatus.atk;
        baseStatus.def = baseStatus.commonStatus.def;
        baseStatus.moveSpeed = baseStatus.commonStatus.moveSpeed;
        baseStatus.attackSpeed  = baseStatus.commonStatus.attackSpeed;
        baseStatus.exp = baseStatus.commonStatus.exp;
        baseStatus.jumpPower = baseStatus.commonStatus.jumpPower;
        baseStatus.jumpCooldown = baseStatus.commonStatus.jumpCooldown;
        baseStatus.target = baseStatus.commonStatus.target;
    }

    // status값 변화 받기
    private void UpdateStats(Func<float, float, float> operation, CharacterStatus newModifier)
    {
        CurrentStatus.maxHealth = operation(CurrentStatus.maxHealth, newModifier.maxHealth);
        CurrentStatus.atk = operation(CurrentStatus.atk, newModifier.atk);
        CurrentStatus.def = operation(CurrentStatus.def, newModifier.def);
        CurrentStatus.attackSpeed = operation(CurrentStatus.attackSpeed, newModifier.attackSpeed);
        CurrentStatus.moveSpeed = operation(CurrentStatus.moveSpeed, newModifier.moveSpeed);
        CurrentStatus.jumpPower = operation(CurrentStatus.jumpPower, newModifier.jumpPower);

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
