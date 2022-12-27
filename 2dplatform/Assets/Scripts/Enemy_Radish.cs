using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Radish : Enemy
{
    

    protected override void Start()
    {
        base.Start();
    }


   
    void Update()
    {
        WalkAround();
        CollisionCheck();
    }
}
