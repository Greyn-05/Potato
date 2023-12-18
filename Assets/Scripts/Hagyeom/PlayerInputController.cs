using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : CharacterController
{
    #region InputEvent
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnJump()
    {
        CallJumpEvent();
    }

    public void OnLook(InputValue value)
    {
        Vector2 lookInput = value.Get<Vector2>().normalized;
        CallLookEvent(lookInput);
    }
    #endregion
}
