using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    #region FIeld
    private CharacterStatusHandler _status;

    public event Action<Vector2> OnMoveEvent;
    public event Action OnJumpEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnAttackEvent;

    protected bool IsAttacking { get; set; }
    private float _timeSinceLastAttack = float.MaxValue;
    private float _coolTime;
    #endregion


    #region Init
    private void Awake()
    {
        _status = GetComponent<CharacterStatusHandler>();
    }
    private void Start()
    {
        SetCoolTime();
    }
    private void Update()
    {
        HandleAttackDelay();
    }
    #endregion


    #region PlayerMovement
    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallJumpEvent()
    {
        OnJumpEvent?.Invoke();
    }

    public void CallLookEvent(Vector2 lookDirection)
    {
        OnLookEvent?.Invoke(lookDirection);
    }
    #endregion


    #region PlayerAttack
    private void SetCoolTime()
    {
        _coolTime = 1 / _status.CurrentStatus.commonStatus.attackSpeed;
    }

    private void HandleAttackDelay()
    {
        if (_timeSinceLastAttack <= _coolTime)    // TODO
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        if (IsAttacking && _timeSinceLastAttack > _coolTime)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent();
        }
    }

    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
    #endregion
}
