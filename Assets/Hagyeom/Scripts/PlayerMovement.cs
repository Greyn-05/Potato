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
    #endregion

    #region Init
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
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


    private void SetMoveDirection(Vector2 direction)
    {
        this._direction = direction;
    }

    private void Move(Vector2 direction)
    {
        direction = direction * 5;
        _rigidbody.velocity = direction;
    }


}
