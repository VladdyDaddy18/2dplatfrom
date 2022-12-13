using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rino : Enemy
{

    [SerializeField] private float speed;
    [SerializeField] private float idleTime = 2;
                     private float idleTimeCounter;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
    }
}
