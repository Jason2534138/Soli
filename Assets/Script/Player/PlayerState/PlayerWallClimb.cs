using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimb : BaseState
{
    private PlayerMovementSM _sm;
    private Detector _detector;

    [Header("Climb Settings")]
    private float climbSpeed = 3f;
    private float slideSpeed = 2f;
    private float originalGravity;
    private float _verticalInput;
    private float _horizontalInput;

    public PlayerWallClimb(PlayerMovementSM stateMachine) : base("PlayerWallClimb", stateMachine)
    {
        _sm = (PlayerMovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _detector = _sm.GetComponent<Detector>();
        originalGravity = _sm.rb.gravityScale;

        // 關閉重力以便控制垂直移動
        _sm.rb.gravityScale = 0f;
        _sm.rb.velocity = Vector2.zero;

        _sm.animator.Play("Player_wall climb");
    }

    public override void Exit()
    {
        base.Exit();
        // 回復正常重力
        _sm.rb.gravityScale = originalGravity;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _verticalInput = Input.GetAxisRaw("Vertical");
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        // 取得角色面向方向（右邊 = 1, 左邊 = -1）
        float facingDir = _sm.isFacingRight ? 1f : -1f;

        // ✅ 離開爬牆的條件：
        // 1. 離開牆面
        // 2. 按的方向鍵 ≠ 貼牆方向（放開或反方向）
        // 3. 碰到地板
        if (!_detector.IsTouchingWall() ||
            Mathf.Sign(_horizontalInput) != facingDir ||
            _detector.IsGrounded())
        {
            stateMachine.ChangeState(_sm.airState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // 根據垂直輸入決定上/下移動
        if (_verticalInput > 0)
        {
            _sm.rb.velocity = new Vector2(0, climbSpeed);
        }
        else if (_verticalInput < 0)
        {
            _sm.rb.velocity = new Vector2(0, -slideSpeed);
        }
        else
        {
            _sm.rb.velocity = Vector2.zero;
        }
    }
}

