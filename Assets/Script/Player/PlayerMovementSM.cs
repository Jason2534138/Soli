using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerMovementSM : StateMachine
{
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

    public bool isFacingRight = true;

    public Animator animator;
    public Rigidbody2D rb;
      
    public PlayerHealth _playerHealth;
    public float speed = 4f;
    public MP _mp;
    

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        idleState = new PlayerIdle(this);
        movingState = new PlayerMoving(this);
        groundComboState = new PlayerGroundCombo(this);
        airState = new PlayerAirState(this);
        airComboState = new PlayerAirCombo(this);
        spearBlockState = new PlayerSpearBlock(this);
        hurtState = new PlayerHurtkState(this);
        _mp = GetComponent<MP>();
        _playerHealth = GetComponent<PlayerHealth>();
        
           
    }


    public void OnHit(int Damage, int Stun, Vector2 knockBack, int onRight)
    {
        if (spearBlockState.isBlocking)
        {
            //ChangeState(this.spearBlockState);
            Debug.Log("blocked");
            
            _mp.playerMPSystem.MPUp(20);
            this.ChangeState(this.idleState);
        }
        else
        {
            ChangeState(hurtState);
            Vector2 posKnockBack;
            posKnockBack = new Vector2(knockBack.x * onRight, knockBack.y);

            _playerHealth.healthSystem.Damage(Damage);
            
            rb.velocity += posKnockBack;
        }

        

    }
    protected override BaseState GetInitialState()
    {
        return idleState;
    }
    
}
