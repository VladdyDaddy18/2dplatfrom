using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : Enemy
{

    [Header("Bat Specifics")]
    [SerializeField] private Transform[] idlepoints;

    private Vector2 destination; 
    private bool canBeAgressive = true;
    private bool playerDetected;
    private Transform player;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsPlayer;

    float defaultSpeed;

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player").transform;
        defaultSpeed = speed;
        destination = idlepoints[0].position;
        transform.position = idlepoints[0].position;
    }
       void Update()
    {

            idleTimeCounter -= Time.deltaTime;
            anim.SetBool("canBeAgressive", canBeAgressive);
            anim.SetFloat("speed", speed);


            if (idleTimeCounter > 0)
                return;
        
            playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);

            if(playerDetected && !aggresive && canBeAgressive)
            {
                aggresive = true;
                canBeAgressive = false;
                destination = player.transform.position;
            }

            if (aggresive)
            {
                transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, destination) < .1f)
                {
                    aggresive = false;
                    int i = Random.Range(0, idlepoints.Length);
                    destination = idlepoints[i].position;
                    speed = speed * .5f;
                    
                }

            }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed  * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) < .1f)
             {
                if (!canBeAgressive)
                    idleTimeCounter = idleTime;

                canBeAgressive = true;
                speed = defaultSpeed;


             }
        }

        FlipContorller();

    }


    public override void Damage()
    {
        base.Damage();
        idleTimeCounter -= 5f;
    }
    private void FlipContorller()
    {
        if ( facingDirection == -1 && transform.position.x < destination.x)
                Flip();
        else if (facingDirection == 1 && transform.position.x > destination.x)
                Flip();

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
