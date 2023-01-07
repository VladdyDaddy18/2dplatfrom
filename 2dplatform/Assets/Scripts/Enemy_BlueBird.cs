using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BlueBird : Enemy
{



    private RaycastHit2D ceilingDetected;

    [Header ("Blue Bird Specifics")]
    [SerializeField] private float ceilingDistance;
    [SerializeField] private float floorDistance;
    [SerializeField] private float flyUpForce;
    [SerializeField] private float flyDownForce;
                     private float flyForce;

    private bool canFly = true;
   
    protected override void Start()
    {
        flyForce = flyUpForce;
        base.Start();
    }

    private void Update() 
    {

        CollisionCheck();

        if(ceilingDetected)
            flyForce = flyDownForce;

        else if (groundDetected)
            flyForce = flyUpForce;
        
        if(wallDetected)
            Flip();
    }

    public void FlyUpEvent()
    {
        if(canFly)
            rb.velocity = new Vector2(speed * facingDirection, flyForce);
    }

    
    public override void Damage()
    {
        canFly = false;   
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0,0);
        base.Damage();
    }

     protected override void CollisionCheck()
    {
        base.CollisionCheck();

        ceilingDetected = Physics2D.Raycast(transform.position, Vector2.up, ceilingDistance, whatIsGround);
        

    }
   
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + ceilingDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - floorDistance));

   }



}


