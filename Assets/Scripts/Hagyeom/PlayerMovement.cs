using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Field
    private CharacterController _controller;
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private CharacterStatusHandler _status;
    private HealthSystem _healthSystem;
    private float lastJumpTime;
    private float _knockbackDuration = 0.0f;
    private Vector2 _knockback = Vector2.zero;
    

    #endregion

    #region Init
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _status = GetComponent<CharacterStatusHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += SetMoveDirection;
        _controller.OnJumpEvent += Jump;
        _controller.OnLookEvent += FlipPlayer;
        lastJumpTime = Time.time - _status.CurrentStatus.jumpCooldown;
    }

    

    private void FixedUpdate()
    {
        Move(_direction);
        if (_knockbackDuration > 0.0f)
        {
            _knockbackDuration -= Time.fixedDeltaTime;
        }

    }
    #endregion

    #region Move
    private void SetMoveDirection(Vector2 direction)
    {
        this._direction = direction;
    }

    private void Move(Vector2 direction)
    {
        Vector2 currentVelocity = _rigidbody.velocity;
        currentVelocity.x = direction.x * _status.CurrentStatus.moveSpeed;
        if (_knockbackDuration > 0.0f)
        {
            currentVelocity.x = _knockback.x;
        }
        _rigidbody.velocity = currentVelocity;
    }

    public void SetKnockback(Transform other)
    {
        _knockbackDuration = 0.3f;
        _knockback = -(other.position - transform.position).normalized * 15f;
    }
    #endregion

    #region Jump
    private void Jump()
    {
        if (Time.time > lastJumpTime + _status.CurrentStatus.jumpCooldown)
        {
            _rigidbody.AddForce(Vector2.up * _status.CurrentStatus.jumpPower, ForceMode2D.Impulse);
            lastJumpTime = Time.time;
        }
    }
    #endregion

    #region LookFlip
    private void FlipPlayer(Vector2 lookDirection)
    {
        float PlayerX = gameObject.transform.localScale.x;
        if((lookDirection.x > 0 && PlayerX > 0f) || (lookDirection.x < 0 && PlayerX < 0f))
        {
            gameObject.transform.localScale = new Vector3 (-gameObject.transform.localScale.x, 1, 1);
        }
    }
    #endregion
}
