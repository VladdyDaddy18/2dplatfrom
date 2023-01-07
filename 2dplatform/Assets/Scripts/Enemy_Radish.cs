using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Radish : Enemy
{

    private RaycastHit2D groundBelowDetected;
    private RaycastHit2D groundAboveDetected;

    [Header ("Radish Specifics")]
    [SerializeField] private float ceilingDistance;
    [SerializeField] private float floorDistance;


    [SerializeField] private float aggroTime;
    [SerializeField] private float aggroTimeCounter;
                     private bool aggresive; 
                        
    [SerializeField] private float flyForce;


    protected override void Start()
    {
        base.Start();
        flyForce = 2;
    }

    public override void Damage()
    {
        

        if (!aggresive)
        {
            aggroTimeCounter = aggroTime;
            rb.gravityScale = 12;
            aggresive = true;
        }
        else
        {
            base.Damage();
        }
    }

    void Update()
    {

        aggroTimeCounter -= Time.deltaTime;

        if (aggroTimeCounter < 0 && !groundAboveDetected)
            {
                rb.gravityScale = 1;
                aggresive = false;
            }


        if(!aggresive)
        {
            if (groundBelowDetected && !groundAboveDetected)
            {
                rb.velocity = new Vector2(0,flyForce);
            }
        }

        else
        {
            if(groundBelowDetected.distance < 1.25f)
                WalkAround();
        }   

       anim.SetFloat("xVelocity", rb.velocity.x);
       anim.SetBool("aggressive", aggresive);
        CollisionCheck();
    }

    
    protected override void CollisionCheck()
    {
        base.CollisionCheck();

        groundAboveDetected = Physics2D.Raycast(transform.position, Vector2.up, ceilingDistance, whatIsGround);
        groundBelowDetected = Physics2D.Raycast(transform.position, Vector2.down, floorDistance, whatIsGround);

        
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + ceilingDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - floorDistance));
    }
}