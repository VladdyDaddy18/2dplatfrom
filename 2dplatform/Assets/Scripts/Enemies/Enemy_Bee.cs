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
    [SerializeField] private float aggroSpeed;

    private bool playerDetected;
    private int idlePointIndex;

    private float defaultSpeed;
    

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
        bool idle = idleTimeCounter > 0;

        anim.SetBool("idle", idle);
        idleTimeCounter -= Time.deltaTime;

        if (idle)
            return;
        if(idleTimeCounter > 0)
        {
            return;
        }

        playerDetected = Physics2D.OverlapCircle(playerCheck.position, checkRadius, whatIsPlayer);

        if (player == null)
            return;

        if(playerDetected && !aggresive)
        {
            aggresive = true;
            speed = aggroSpeed;
        }

        if (!aggresive)
        {
            transform.position = Vector2.MoveTowards(transform.position, idlepoints[idlePointIndex].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, idlepoints[idlePointIndex].position) < .1f)
            {
                idlePointIndex++;

                if(idlePointIndex >= idlepoints.Length)
                    idlePointIndex = 0;
            }
        
        }
        else
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
     private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);

        newBullet.GetComponent<Bullet>().SetupSpeed(0, -bulletSpeed);
        Destroy(newBullet, 2f);

        idleTimeCounter = idleTime;
        aggresive = false;
        speed = defaultSpeed;
    }

   

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(playerCheck.position, checkRadius);
    }
}
