using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : Enemy
{

    [Header("Bee Specifics")]
    [SerializeField] private Transform[] idlepoints;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float _yOffset;

    private bool playerDetected;
    private Transform player;
    private float defaultSpeed;
    private bool attackOver;

    [Header("Bullet Specifics")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private float bulletSpeed;

    protected override void Start()
    {
        defaultSpeed = speed;   
        base.Start();
    }



    // Update is called once per frame
    void Update()
    {
        idleTimeCounter -= Time.deltaTime;
        if(idleTimeCounter > 0)
        {
            return;
        }

        playerDetected = Physics2D.OverlapCircle(playerCheck.position, checkRadius, whatIsPlayer);

        if(playerDetected)
        {
            aggresive = true;
        }

        if (!aggresive)
        {
            transform.position = Vector2.MoveTowards(transform.position, idlepoints[0].position, speed * Time.deltaTime);
        }
        else
        {
            if(!attackOver)
            {
                Vector2 newPosition = new Vector2(player.transform.position.x, player.transform.position.y + _yOffset);
                transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

                float xDifference = transform.position.x - player. position.x;
                if(Mathf.Abs(xDifference) < .15f)
                {
                    anim.SetTrigger("attack");

                }
               
            }
        }
    }
     private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);

        newBullet.GetComponent<Bullet>().SetupSpeed(0, -bulletSpeed);

        idleTimeCounter = idleTime;
        aggresive = false;
    }
}
