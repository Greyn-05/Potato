using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Field
    private CharacterController _controller;
    private CharacterStatusHandler _status;

    [SerializeField] private Transform _pos;
    [SerializeField] private Vector2 _boxSize;
    #endregion


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _status = GetComponent<CharacterStatusHandler>();
    }

    private void Start()
    {
        _controller.OnAttackEvent += Attck;
    }


    
    private void Attck()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_pos.position, _boxSize, 0);
        
        foreach(Collider2D collider in colliders)
        {
            Debug.Log(collider.tag);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_pos.position, _boxSize);
    }
}
