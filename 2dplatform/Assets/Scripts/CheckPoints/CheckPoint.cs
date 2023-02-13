using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D collision)
   {
    if (collision.GetComponent<Player>() != null)
        {
            GetComponent<Animator>().SetTrigger("activated");
            Player_Manager.instance.respawnPoint = transform;
        }
   }
}
