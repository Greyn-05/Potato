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
    private float lastJumpTime;

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
        _controller.OnLookEvent += FlipPlayer;
        lastJumpTime = Time.time - _status.CurrentStatus.commonStatus.jumpCooldown;
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
        currentVelocity.x = direction.x * _status.CurrentStatus.commonStatus.moveSpeed;
        _rigidbody.velocity = currentVelocity;
    }
    #endregion

    #region Jump
    private void Jump()
    {
        if (Time.time > lastJumpTime + _status.CurrentStatus.commonStatus.jumpCooldown)
        {
            _rigidbody.AddForce(Vector2.up * _status.CurrentStatus.commonStatus.jumpPower, ForceMode2D.Impulse);
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
