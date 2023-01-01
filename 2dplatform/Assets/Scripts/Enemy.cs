using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected Transform groundCheck;

    protected bool wallDetected;
    protected bool groundDetected;

    [HideInInspector] public bool invincible;

    [SerializeField] protected float speed;
    [SerializeField] protected float idleTime = 2;
                     protected float idleTimeCounter;



    protected Animator anim;
    protected Rigidbody2D rb;

    protected int facingDirection = -1;

    protected virtual void Start() 
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();    
    }


//making new function for walkaround to speed up new enemy scripts.
    protected virtual void WalkAround()
    {
        if(idleTimeCounter <= 0)
                rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y); 
            else
                rb.velocity = Vector2.zero;  

            idleTimeCounter -= Time.deltaTime; 

            

            if(wallDetected || !groundDetected)
            {
                idleTimeCounter = idleTime;
                Flip();
            }
    }
   
    public void Damage()
    {
        if(!invincible)
             anim.SetTrigger("gotHit");
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision ) 
        
    {
        if(collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();

            player.KnockBack(transform);
        }
    }

    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0,180,0);
    }

    protected virtual void CollisionCheck()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, groundCheckDistance, whatIsGround);
    }

    protected virtual void OnDrawGizmos() 
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
    }
}
