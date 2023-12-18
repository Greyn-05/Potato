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
    #endregion

    #region Init
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _status = GetComponent<CharacterStatusHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += SetMoveDirection;
        _controller.OnJumpEvent += Jump;
    }

    private void FixedUpdate()
    {
        Move(_direction);

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
        _rigidbody.velocity = currentVelocity;
    }
    #endregion

    #region Jump
    private void Jump()
    {
        Debug.Log(_status.CurrentStatus.jumpPower);
        _rigidbody.AddForce(Vector2.up * _status.CurrentStatus.jumpPower, ForceMode2D.Impulse);
    }

    #endregion

}
