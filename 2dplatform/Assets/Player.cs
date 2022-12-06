using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
   private Animator anim;
   private Rigidbody2D rb;
   
   [Header ("Move Info")]
   public float moveSpeed;
   public float jumpForce;
   private bool canDoubleJump;
   private float movingInput;
   [SerializeField] private float bufferJumpTime;
                    private float bufferJumpCounter;

   [SerializeField] private float cayoteJumpTime;
                    private float cayoteJumpCounter;
                    private bool canHaveCoyoteJump;
   private bool canMove;
   public Vector2 wallJumpDirection;
   public float doubleJumpForce;

   private float defaultJumpForce;

   [Header("Knocked back info")]
   [SerializeField] private Vector2 knockBackDirection;
   [SerializeField] private float knockBackTime;
   [SerializeField] private float knockBackProtectionTime;
   private bool isKnocked;
   private bool canBeKnocked = true;

   [Header ("Collison Info")]
   public LayerMask whatIsGround;
   public float groundCheckDistance;
   public bool isGrounded;
   //Attempt at wall detection

   private bool isWallDetected;

   public float wallCheckDistance;  
   private bool canWallSlide;
   private bool isWallSliding;
   
   //end collison for wall//

   private bool facingRight = true;
   private int facingDirection = 1;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        defaultJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {

        //switching to moving animation
        AnimationControllers();
        if(isKnocked)
            return;

        CollisionCheck();
        InputCheck();
        FlipController();

        bufferJumpCounter -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;
        
    if(isGrounded)
            {
                canDoubleJump = true;
                canMove = true;

                if(bufferJumpCounter > 0)
                    {
                        bufferJumpCounter = - 1;
                        Jump();
                    }
                canHaveCoyoteJump = true;
            }
            else
            {
                if (canHaveCoyoteJump)
                {
                    canHaveCoyoteJump = false;
                    cayoteJumpCounter = cayoteJumpTime;
                }
            }      
        if(canWallSlide)
            {
                //Attempt to fix double jump bug//
                canDoubleJump = true;
                //Good Fix//
            
                isWallSliding = true;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .01f);
            } 


        Move();
        
    
    }


//Extracted Methods//

private void Flip()
{
    facingDirection = facingDirection * -1;
    facingRight = !facingRight;
    transform.Rotate(0,180,0);
}
private void FlipController()
{
    if(facingRight && rb.velocity.x < 0)
    {
        Flip();

    }
    else if (!facingRight && rb.velocity.x > 0)
    {
        Flip();
    }
}
    
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Move()
    {
        if(canMove)
        rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
    }

    private void wallJump()
    {
       canMove = false;
       rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y); 
    }
    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);

        if(isWallDetected && rb.velocity.y < 0)
        {
            canWallSlide = true;
        }
        if(!isWallDetected)
        {
            isWallSliding = false;
            canWallSlide = false;
        }

    }
    private void InputCheck()
    {
         movingInput = Input.GetAxis("Horizontal");

         if(Input.GetAxis("Vertical") < 0)
            canWallSlide = false;
            
        if(Input.GetKeyDown(KeyCode.Space))
            JumpButton();
    }
    private void JumpButton()
    {
        if(!isGrounded)
            bufferJumpCounter = bufferJumpTime;
        if(isWallSliding)
        {
            wallJump();
            canDoubleJump = true;
        }
        else if(isGrounded || cayoteJumpCounter >0)
            {
             Jump();
            }
            
            else if (canDoubleJump)
            {
                canMove = true;
                canDoubleJump = false;
                jumpForce = doubleJumpForce;
                Jump();
                jumpForce = defaultJumpForce;
            }
            canWallSlide = false;
    }

    public void KnockBack(int direction)
    {
        if(!canBeKnocked)
            return;

        isKnocked = true;
        canBeKnocked = false;
        rb.velocity = new Vector2(knockBackDirection.x * direction, knockBackDirection.y);

        Invoke("CancelKnockBack", knockBackTime);
        Invoke("AllowKnockBack", knockBackProtectionTime);
    }

    private void CancelKnockBack()
    {
        isKnocked = false;
    }

    private void AllowKnockBack()
    {
        canBeKnocked = true;
    }
    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isKnocked", isKnocked);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isWallDetected", isWallDetected);
        anim.SetFloat("yVelocity", rb.velocity.y);

    }


//This is used to draw the line to see the ground check
    private void OnDrawGizmos() 
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance, transform.position.z));

        //Attempt at collision for wall//
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
    }
}
