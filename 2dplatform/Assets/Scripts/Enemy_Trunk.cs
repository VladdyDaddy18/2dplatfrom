using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trunk : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
