using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _controller;
    private HealthSystem _healthSystem;

    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _controller = GetComponent<CharacterController>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        _controller.OnAttackEvent += Attacking;
        _controller.OnMoveEvent += Move;
        _healthSystem.OnDamage += Hit;
        _healthSystem.OnDeath += Death;
    }

    private void Attacking()
    {
        _animator.SetTrigger(Attack);
    }

    private void Move(Vector2 value)
    {
        _animator.SetBool(IsWalking, value.magnitude>0);
    }

    private void Hit()
    {
        _animator.SetTrigger(IsHit);
    }

    private void Death()
    {
        _animator.SetTrigger(IsDead);
    }
}
