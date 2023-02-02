using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trunk : Enemy
{


    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private float bulletSpeed;

    private bool playerDetected;

    [Header("Bullet Specifics")]
    [SerializeField] private float attackCoolDown;
                     private float attackCoolDownCounter;
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

        if (playerDetection.collider.GetComponent<Player>() != null)
        {
            if(attackCoolDownCounter < 0)
            {
                 attackCoolDownCounter = attackCoolDown;
                 anim.SetTrigger("attack");
                 canMove = false;
            }
            
        }
        else
            {
                WalkAround();
            }

    

    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

         if(groundCheck != null)
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        
        if(wallCheck != null)
           {
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetection.distance * facingDirection, wallCheck.position.y));
           }

        //Gizmos.DrawWireSphere(playerCheck.position, checkRadius);
    }

     protected override void CollisionCheck()
    {
        base.CollisionCheck();
        playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
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

