using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
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
    [HideInInspector] public float jumpPower;
    [HideInInspector] public float jumpCooldown;
    public TagData tag;

    public CommonStatus commonStatus;
    #endregion
}
