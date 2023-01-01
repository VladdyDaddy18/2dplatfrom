using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Radish : Enemy
{

    private bool groundBelowDetected;
    private bool groundAboveDetected;

    [Header ("Radish Specifics")]
    [SerializeField] private float ceilingDistance;
    [SerializeField] private float floorDistance;

    [SerializeField] private bool aggresive;



    protected override void Start()
    {
        base.Start();
    }


   
    void Update()
    {
        if(!aggresive)
        {
            if (groundBelowDetected && !groundAboveDetected)
            {
                rb.velocity = new Vector2(0,1);
            }
        }

        else
        {
            if(groundDetected)
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