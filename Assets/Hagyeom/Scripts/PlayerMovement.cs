using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
        direction = direction * _status.CurrentStatus.moveSpeed;
        _rigidbody.velocity = direction;
    }
    #endregion

}
