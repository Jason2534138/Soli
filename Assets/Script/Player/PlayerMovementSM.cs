using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerMovementSM : StateMachine, IDamageable
{
    #region SetUp
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

    public bool isFacingRight = true;

    public Animator animator;
    public Rigidbody2D rb;
      
    public Health _playerHealth;
    public float speed = 4f;
    public MP _mp;
    public float _jumpForce;

    public DroneMovement Drone;

    #endregion


    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
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
        _mp = GetComponent<MP>();
        _playerHealth = GetComponent<Health>();
        _playerHealth.SetUp(100);
        
           
    }
    protected override BaseState GetInitialState()
    {
        return idleState;
    }
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            

        }
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
        rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
    }
    
    private void Interact(GameObject generator)
    {
        
    }
}
