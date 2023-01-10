using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy  
{

    [Header("Ghost Specifics")]
    [SerializeField] private float activeTime;
                     private float activeTimeCounter = 4;

    private Transform player;

    protected override void Start()
    {
        player = GameObject.Find("Player").transform;
        base.Start();
        
    }
   

    void Update()
    {
        activeTimeCounter -= Time.deltaTime;
        idleTimeCounter -= Time.deltaTime;

        if(activeTimeCounter > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if(activeTimeCounter < 0 && idleTimeCounter < 0 && aggresive)
           {
             anim.SetTrigger("disappear");
             aggresive = false;
             idleTimeCounter = idleTime;
           }
        if(idleTimeCounter < 0 && activeTimeCounter < 0 && !aggresive)
           {
            anim.SetTrigger("appear");
            aggresive = true;
            activeTimeCounter = activeTime;

           }
           
        
    }
 public override void Damage()
    {
        base.Damage();
    }
    
}
