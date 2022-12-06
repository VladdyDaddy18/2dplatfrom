using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    public void Damage()
    {
        Debug.Log("I was damaged");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.collider.GetComponent<Player>() != null)
        {
            Player player = collision.collider.GetComponent<Player>();

        if(player.transform.position.x > transform.position.x)
            player.KnockBack(1);
        else if (player.transform.position.x < transform.position.x)
            player.KnockBack(-1);
         }
    }
}
