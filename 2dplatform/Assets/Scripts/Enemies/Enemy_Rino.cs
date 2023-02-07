using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rino : Enemy
{
    [Header ("Rino Specific")]
    [SerializeField] private float aggressiveSpeed;
    [SerializeField] private float shockTime;
                     private float shockTimeCounter;

    //[SerializeField] private LayerMask whatToIgnore;
    
   //private bool aggresive;
    protected override void Start()
    {
        base.Start();
        invincible = true;

        
    }

    // Update is called once per frame
    void Update()
    {

        CollisionCheck();
        
        if(playerDetection.collider.GetComponent<Player>() != null)
            aggresive = true;


        if(!aggresive)
        {

            WalkAround();
            
        }

        else
        {
            if(!groundDetected)
            {
                Flip();
                aggresive = false;
            }
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
                aggresive = false;
            }
            shockTimeCounter -= Time.deltaTime;

        }

        
        AnimationControllers();
        
        
        
    }
 

        private void AnimationControllers()
        {
            anim.SetFloat("xVelocity", rb.velocity.x); 
            anim.SetBool("invincible", invincible);    
        }

}