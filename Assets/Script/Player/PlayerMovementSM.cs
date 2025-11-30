using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerMovementSM : StateMachine, IDamageable
{
    #region States
    [HideInInspector]
    public PlayerIdle idleState;
    [HideInInspector]
    public PlayerMoving movingState;
    [HideInInspector]
    public PlayerGroundCombo groundComboState; 
    [HideInInspector]
    public PlayerDashing dashingState;
    [HideInInspector]
    public PlayerAirCombo airComboState;
    [HideInInspector]
    public PlayerAirState airState;
    [HideInInspector]
    public PlayerSpearBlock spearBlockState;
    [HideInInspector]
    public PlayerHurtkState hurtState;
    [HideInInspector]
    public PlayerBlockAttackState blockAttackState;
    [HideInInspector]
    public PlayerDashing dashing;
    [HideInInspector]
    public PlayerDashAttack dashAttack;
    [HideInInspector]
    public PlayerWallJumpState wallJumpState;
    [HideInInspector]
    public PlayerLedgeClimb playerLedgeClimb;
    #endregion
    [HideInInspector]
    public bool isFacingRight = true;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Health _playerHealth;
    public float speed = 4f;
    [HideInInspector]
    public MP _mp;
    public float _jumpForce;

    public int _maxWallJump;
    [HideInInspector]
    public int _wallJumpLeft;

    public int _maxJump;
    [HideInInspector]
    public int _jumpLeft;

    public  LedgeDetection _ledgeDetection;

    public bool _actionOver = false;


    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        SetUp();
        _mp = GetComponent<MP>();
        _playerHealth = GetComponent<Health>();
        _playerHealth.SetUp(100);
        _ledgeDetection = GetComponentInChildren<LedgeDetection>();
           
    }
    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    public void OnHit(int damage)
    {
        //if (spearBlockState.isBlocking)
        //{
        //    //ChangeState(this.spearBlockState);
        //    Debug.Log("blocked");

        //    _mp.playerMPSystem.MPUp(20);
        //    this.ChangeState(this.idleState);
        //}
        ChangeState(hurtState);
        _playerHealth.healthSystem.Damage(damage);
    }
    public void Pogo()
    {
        Debug.Log("POGO");
        rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
    }
    private void SetUp()
    {
        idleState = new PlayerIdle(this);
        blockAttackState = new PlayerBlockAttackState(this);
        movingState = new PlayerMoving(this);
        groundComboState = new PlayerGroundCombo(this);
        airState = new PlayerAirState(this);
        dashAttack = new PlayerDashAttack(this);
        airComboState = new PlayerAirCombo(this);
        spearBlockState = new PlayerSpearBlock(this);
        hurtState = new PlayerHurtkState(this);
        dashing = new PlayerDashing(this);
        wallJumpState = new PlayerWallJumpState(this);
        playerLedgeClimb = new PlayerLedgeClimb(this);
    }
    public void ActionOver()
    {
        _actionOver = true;
    }
}
