using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{


    private Animator animator;
    private string currentAnimation;

    const string PLAYER_IDLE = "Player_idle 0";
    const string PLAYER_WALK = "Player_walk 0";
    const string PLAYER_RUN = "Player_run 0";
    const string PLAYER_JUMP = "Player_jump_up 0";
    const string PLAYER_DASH = "Player_dash 0";
    const string PLAYER_FALL = "Player_fall 0";
    const string PLAYER_HURT = "Player_hurt";
    const string PLAYER_CROUCH = "Player_crouch 0";
    const string PLAYER_AIM = "Player_aim";
    const string PLAYER_SHOOT = "Player_shoot";
    const string PLAYER_WALLSLIDE = "Player_wallSlide";
    const string PLAYER_ATTACK_Combo1_1 = "Player_attack_combo1_1";
    const string PLAYER_ATTACK_UP_AIR = "Player_attack_up_air";

    private bool isCrouching;
    
    private bool isRunning;

    

    //most attack

    

    private bool isAttacking;
    private bool canAttack = true;
    [SerializeField]
    private float attackBufferTimer;
    private float attackBufferCounter;
    [SerializeField]
    private float comboTime;
    private float comboTimeLeft;
    private int comboCount;
    [SerializeField]
    private float attackDashPower;
    [SerializeField]
    private float attackCombo1Duration;
    [SerializeField]
    private float attackCombo1_cooldown;

    //walljump
    private bool isWallClimbing;
    [SerializeField]
    private float wallSlidingSpeed = 2f;

    [SerializeField]
    private bool wallJunp;
    [SerializeField]
    private bool dashAttack;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.1f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.48f;
    [SerializeField]
    private Vector2 wallJumpingPower = new Vector2(15f, 20f);
    [SerializeField] Transform wallcheck;

    //jump
    [SerializeField]
    private int maxJump;
    private int jumpLeft;
    private bool isJumping;

    //移動&跳躍
    private float horizontal;
    [SerializeField]
    private float speed = 18f;
    
    [SerializeField]
    private float jumpingPower = 16f;
    //殘影衝?E\?E
    private bool canDash = true;
    private bool isDashing;
    [SerializeField]
    private float dashingPower = 24f;
    [SerializeField]
    private float dashTime = 0.2f;
    private float originalGravity;
    [SerializeField]
    private float dashCoolDown = 2f;

    public float distanceBetweenImages;
    private float lastImageXpos;
    private float lastImageYpos;

    //面朝方向
    private bool isFacingRight = true;
    

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 playerScreenPosition;
    private Vector2 mouse;
    private Vector2 diraction;

    private void Start()
    {
        originalGravity = rb.gravityScale;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
        
        
        attackBufferCounter -= Time.deltaTime;
        if (comboTimeLeft >= 0f) comboTimeLeft -= Time.deltaTime;
        

        AfterImage();
        if (isAttacking)
        {
            
            
            return;
        }
        if (canAttack && attackBufferCounter > 0f)
        {
            
            attackBufferCounter = 0f;
            
            canAttack = false;
        }
        
        if (IsGrounded()) jumpLeft = maxJump;
        WallClimb();
        WallJump();
        CheckInput();
        if (isDashing) return;
        if (rb.velocity.y < -0.01f && !IsGrounded() && !isWallClimbing && !isAttacking) 
        {
            ChangeAnimtionState(PLAYER_FALL);
        }
        if (isWallClimbing && IsWalled()) ChangeAnimtionState(PLAYER_WALLSLIDE);
        

        //CheckInput();

        //if (isWallJumping) return;
        //    else Flip();
    }
    private void LateUpdate()
    {
        //Debug.Log(horizontal);
        if (isWallJumping && !isAttacking || isDashing || isAttacking) return;
        else Flip();
    }

    private void FixedUpdate()
    {
        
        if (isDashing || isAttacking) return;
        
        //if (!IsGrounded()) return;
        if (isWallJumping) 
        {
          return;
        } else rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    }
    private void CheckInput()
    {
       
        horizontal = Input.GetAxisRaw("Horizontal");


        if (Input.GetButtonDown("Attack")) 
        {
            attackBufferCounter = attackBufferTimer;          
        }
        if (Input.GetButton("Down") && !isDashing && IsGrounded() && !isJumping)
        {
            horizontal = 0;
            isCrouching = true;
            ChangeAnimtionState (PLAYER_CROUCH);
        }else isCrouching = false;

        if (Input.GetButtonDown("Jump") && !isDashing && jumpLeft > 0 && !isWallClimbing)
        {
            if (!IsGrounded() && !isWallClimbing) 
            {
                jumpLeft--;
                //isWallJumping = false;
                isJumping = true;
                
            }
            
            if (jumpLeft > 0 && !isWallClimbing)
            {
                isJumping = true;

                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                ChangeAnimtionState(PLAYER_JUMP);
            }
            
        }else if (rb.velocity.y < 0f) isJumping = false;

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f && !isWallJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
            //PlayerAfterImagePool.Instance.GetFromPool();
            //lastImageXpos = transform.position.x;
        }
        if (horizontal == 0 && !isCrouching && !isDashing && IsGrounded() && !isJumping && !isAttacking) ChangeAnimtionState(PLAYER_IDLE);
        if (horizontal != 0 && IsGrounded() && !isCrouching && !isRunning && !isDashing && !isJumping && !isAttacking) ChangeAnimtionState(PLAYER_RUN);
        
    }
    
   
    private bool IsGrounded() 
    {
        
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
     
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            //transform.Rotate(0f, 180f, 0f);
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale; 
        }
    }
    private IEnumerator Dash()
    {
        

        Debug.Log(horizontal);
        Debug.Log(isFacingRight);
        if (horizontal != 0)
        {
            rb.velocity = new Vector2(horizontal * dashingPower, 0f);
            Flip();
            canDash = false;
            isDashing = true;
            rb.gravityScale = 0f;
            ChangeAnimtionState(PLAYER_DASH);

        }
        else if (Input.GetButton("Horizontal") == false)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            canDash = false;
            isDashing = true;
            rb.gravityScale = 0f;
            ChangeAnimtionState(PLAYER_DASH);
        }
            



        
        yield return new WaitForSeconds(dashTime);
        
        rb.gravityScale = originalGravity;
        
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
    private void AfterImage()
    {
        if ((Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages || Mathf.Abs(transform.position.y - lastImageYpos) > distanceBetweenImages) && (isDashing /*|| currentAnimation == PLAYER_FALL || currentAnimation == PLAYER_JUMP || currentAnimation == PLAYER_ATTACK_UP_AIR || isAttacking*/))
        {
            PlayerAfterImagePool.Instance.GetFromPool();
            lastImageXpos = transform.position.x;
            lastImageYpos = transform.position.y;
        }
            
    }

    private void ChangeAnimtionState(string newState)
    {
        if (currentAnimation == newState) return;

        animator.Play(newState);

        currentAnimation = newState;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallcheck.position, 0.5f, groundLayer);
    }
    private void WallClimb()
    {
        
        if (IsWalled() && !IsGrounded() && Input.GetButton("Horizontal") && ((rb.velocity.y < 0.1f) || Input.GetButton("Up") && isWallClimbing == true))
        {
            
            isWallClimbing = true;
            jumpLeft = maxJump;
            /*if (Input.GetButton("Vertical"))
            {
                if (Input.GetButton("Up"))
                {   
                    rb.velocity = new Vector2(0f, 10f);
                }
                    
                else if (Input.GetButton("Down")) rb.velocity = new Vector2(0f, -10f);
            }
            else */
            rb.velocity = new Vector2(0f, wallSlidingSpeed * -1);
            rb.gravityScale = 0f;
        }  
        else
        {
            isWallClimbing = false;
            if(!isDashing)rb.gravityScale = originalGravity;
        }
    }
    
    private void WallJump()
    {
        if (wallJunp)
        {
            if (isWallClimbing)
            {
                isWallJumping = false;
                if (horizontal != 0) wallJumpingDirection = horizontal * -1f;
                else
                {
                    if (isFacingRight) wallJumpingDirection = -1f;
                    else wallJumpingDirection = 1f;
                }
                wallJumpingCounter = wallJumpingTime;

                CancelInvoke(nameof(StopWallJumping));
            }
            else
            {
                wallJumpingCounter -= Time.deltaTime;
            }
            if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f && isWallClimbing)
            {
                isWallJumping = true;
                rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
                wallJumpingCounter = 0f;

                if (true)
                {
                    if (horizontal != 0) horizontal *= -1f;
                    else
                    {
                        if (isFacingRight) horizontal = -1f;
                        else horizontal = 1f;
                    }
                    Flip();
                    ChangeAnimtionState(PLAYER_JUMP);
                    canDash = true;
                }

                Invoke(nameof(StopWallJumping), wallJumpingDuration);
            }
        }
        
    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    

}
