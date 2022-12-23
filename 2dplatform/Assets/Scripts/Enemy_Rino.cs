using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rino : Enemy
{

    [SerializeField] private float speed;
    [SerializeField] private float aggressiveSpeed;
    [SerializeField] private float idleTime = 2;
                     private float idleTimeCounter;

    [SerializeField] private LayerMask whatIsPlayer;
    private bool playerDetected;
    private bool isAggressive;
    protected override void Start()
    {
        base.Start();
        invincible = true;
    }

    // Update is called once per frame
    void Update()
    {

        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection,100, whatIsPlayer);
        if(playerDetected)
            isAggressive = true;


        if(!isAggressive)
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

        else
            rb.velocity = new Vector2(aggressiveSpeed * facingDirection, rb.velocity.y);
        
        CollisionCheck();
    
        anim.SetFloat("xVelocity", rb.velocity.x);
    }
}

