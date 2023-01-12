using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy  
{

    [Header("Ghost Specifics")]
    [SerializeField] private float activeTime;
                     private float activeTimeCounter = 4;

    private Transform player;
    private SpriteRenderer sr;

    protected override void Start()
    {
        player = GameObject.Find("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        base.Start();
        aggresive = true;
        invincible = true;
        
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
            ChoosePosition();
            anim.SetTrigger("appear");
            aggresive = true;
            activeTimeCounter = activeTime;

           }
        
        if ( facingDirection == -1 && transform.position.x < player.transform.position.x)
                Flip();
        else if (facingDirection == 1 && transform.position.x > player.transform.position.x)
                Flip();
    }

    private void ChoosePosition()
    {
        transform.position = new Vector2(player.transform.position.x + 7, player.transform.position.y + 7);
    }

    public void Disappear()
    {
        sr.enabled = false;
    }

    public void Appear()
    {
        sr.enabled = true;
    }


 

protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(aggresive)
            base.OnTriggerEnter2D(collision);
    }
    
}
