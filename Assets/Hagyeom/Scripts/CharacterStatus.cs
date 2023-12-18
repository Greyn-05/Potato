using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusChangeType
{
    ADD,
    MULTIPLE,
    OVERRIDE
}

[Serializable]
public class CharacterStatus
{
    #region Field
    public StatusChangeType statusChangeType;
    [HideInInspector] public float maxHealth;
    [HideInInspector] public float hp;
    [HideInInspector] public float atk;
    [HideInInspector] public float def;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float attackSpeed;
    [HideInInspector] public float exp;
    public LayerMask target;

    public CommonStatus commonStatus;
    #endregion
}
