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


   [Header ("Collison Info")]
   public LayerMask whatIsGround;
   public float groundCheckDistance;
   public bool isGrounded;

   private bool facingRight = true;
   private int facingDirection = 1;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //switching to moving animation

        

        AnimationControllers();
        CollisionCheck();
        InputCheck();
        FlipController();

        
    
    

         if(Input.GetKeyDown(KeyCode.Space))//whole if statement is  JumpButton()
        {
            JumpButton();
        }
    
        if(isGrounded)
            {
                canDoubleJump = true;
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
    if(facingRight && movingInput < 0)
    {
        Flip();

    }
    else if (!facingRight && movingInput > 0)
    {
        Flip();
    }
}
    private void JumpButton()
    {
        if(isGrounded)
            {
             Jump();
            }
            
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                Jump();
            }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    private void InputCheck()
    {
         movingInput = Input.GetAxis("Horizontal");
    }
    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);

    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance, transform.position.z));
    }
}
