using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    [SerializeField]
    private float idleTime;
    [SerializeField]
    private float walkTime;
    [SerializeField]
    private float walkSpeed;

    private float idleTimer;
    private float walkTimer;


    private bool isFacingRight = false;
    private bool isWalking = true;

    Rigidbody2D rb;
    Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (isWalking == false) rb.velocity = new Vector2 (0, rb.velocity.y);
        else
        {
            if (isFacingRight == false) rb.velocity = new Vector2 (walkSpeed * -1f, rb.velocity.y);
            else rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        idleTimer -= Time.deltaTime;
        if (walkTimer > 0)
        {
            walkTimer -= Time.deltaTime;
            isWalking = true;
            idleTimer = Random.Range(0f, idleTime);
            animator.SetBool("isMoving", true);
        }
        else if (idleTimer > 0)
        {
            idleTimer -= Time.deltaTime;
            isWalking = false;

            animator.SetBool("isMoving", false);
        }
        else
        {
            int flip = Random.Range(0, 2);
            
            if (flip == 1) 
            {
                Vector3 localscale = transform.localScale;
                localscale.x *= -1f;
                transform.localScale = localscale;
                isFacingRight = !isFacingRight;
            }
           
            walkTimer = Random.Range(0f, walkTime); ;
        }
        
    }
    
}
