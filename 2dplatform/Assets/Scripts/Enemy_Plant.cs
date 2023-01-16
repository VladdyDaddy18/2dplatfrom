using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Plant : Enemy
{
    [Header("Plant spesifics")]

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;

    protected override void Start()
    {
        base.Start();

        if(groundCheck == null)
            groundCheck = transform;
        if(wallCheck == null)
            wallCheck = transform;

    }

    
    void Update()
    {

        CollisionCheck();

        idleTimeCounter -= Time.deltaTime;

        bool playerDetected = playerDetection.collider.GetComponent<Player>() != null;

        if(idleTimeCounter < 0 && playerDetected)
            {
                idleTimeCounter = idleTime;
                anim.SetTrigger("attack");

            }   
    }
 

    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);

        newBullet.GetComponent<Bullet>().SetupSpeed(speed * facingDirection, 0);
    }
}
