using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rino : Enemy
{

    [SerializeField] private float speed;
    [SerializeField] private float aggressiveSpeed;
    [SerializeField] private float idleTime = 2;
                     private float idleTimeCounter;
    [SerializeField] private float shockTime;
                     private float shockTimeCounter;

    [SerializeField] private LayerMask whatToIgnore;
    private RaycastHit2D playerDetection;
    private bool isAggressive;
    protected override void Start()
    {
        base.Start();
        invincible = true;

        
    }

    // Update is called once per frame
    void Update()
    {

        playerDetection = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection,25, ~whatToIgnore);
        if(playerDetection.collider.GetComponent<Player>() != null)
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
        {
            rb.velocity = new Vector2(aggressiveSpeed * facingDirection, rb.velocity.y);

            if(wallDetected && invincible)
            {
                invincible = false;
                shockTimeCounter = shockTime;
            }

            if(shockTimeCounter <= 0 && !invincible)
            {
                invincible = true;
                Flip();
                isAggressive = false;
            }
            shockTimeCounter -= Time.deltaTime;

        }

        CollisionCheck();
        AnimationControllers();
        
        
        
    }
    protected override void OnDrawGizmos() 
        {
          //base.OnDrawGizmos;

          Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetection.distance * facingDirection, wallCheck.position.y));  
        }

        private void AnimationControllers()
        {
            anim.SetFloat("xVelocity", rb.velocity.x); 
            anim.SetBool("invincible", invincible);    
        }

}