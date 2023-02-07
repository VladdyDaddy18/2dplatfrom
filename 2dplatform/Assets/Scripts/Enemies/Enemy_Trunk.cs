using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trunk : Enemy
{

    [Header("Trunk Spec's")]
    [SerializeField] private float moveBackTime;
                     private float moveBackTimeCounter;

    [Header("Collision Spec's")]
    [SerializeField] private Transform groundBehindCheck;
    private bool wallBehind;
    private bool groundBehind;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsPlayer;
     private bool playerDetected;



    [Header("Bullet Specifics")]
    [SerializeField] private float attackCoolDown;
                     private float attackCoolDownCounter;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private float bulletSpeed;

    
    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        CollisionCheck();

        if(!canMove)
        {
            rb.velocity = new Vector2(0,0);
        }

      
       anim.SetFloat("xVelocity", rb.velocity.x);

         attackCoolDownCounter -= Time.deltaTime;
         moveBackTimeCounter -= Time.deltaTime;
        

        if (playerDetected && moveBackTimeCounter < 0)
                 moveBackTimeCounter = moveBackTime;


        if (playerDetection.collider.GetComponent<Player>() != null)
        {
            if(attackCoolDownCounter < 0)
            {
                 attackCoolDownCounter = attackCoolDown;
                 anim.SetTrigger("attack");
                 canMove = false;
            }
            else if(playerDetection.distance < 3)
            {
                WalkBackwards(1.5f);
            }
            
        }
        else
            {
                if(moveBackTimeCounter > 0)
                    WalkBackwards(4);
                else
                    WalkAround();
            }

    

    }
    

    private void WalkBackwards(float multiplier)
    {
        if(wallBehind || !groundBehind)
            return;
        
        rb.velocity = new Vector2(speed * multiplier * -facingDirection, rb.velocity.y);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.DrawWireSphere(transform.position, checkRadius);


         if(groundCheck != null)
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        
        if(wallCheck != null)
           {
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetection.distance * facingDirection, wallCheck.position.y));
           }

        
        Gizmos.DrawLine(groundBehindCheck.position, new Vector2(groundBehindCheck.position.x, groundBehindCheck.position.y - groundCheckDistance));
    }

     protected override void CollisionCheck()
    {
        base.CollisionCheck();
        
        playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        groundBehind = Physics2D.Raycast(groundBehindCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallBehind = Physics2D.Raycast(wallCheck.position, Vector2.right * (-facingDirection + 1), wallCheckDistance, whatIsGround);
    }

      private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
        newBullet.GetComponent<Bullet>().SetupSpeed(bulletSpeed * facingDirection, 0);
        Destroy(newBullet, 2f);
    }

    private void ReturnMovement()
    {
        canMove = true;
    }
}

