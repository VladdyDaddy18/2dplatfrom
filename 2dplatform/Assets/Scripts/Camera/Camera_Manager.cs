using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Camera_Manager : MonoBehaviour
{
   [SerializeField] private GameObject myCamera; 
   [SerializeField] private PolygonCollider2D cd;
   [SerializeField] private Color gizmosColor;

   private void Start() 
   {
    myCamera.GetComponent<CinemachineVirtualCamera>().Follow = Player_Manager.instance.currentPlayer.transform;
   }


   private void OnTriggerEnter2D(Collider2D collision) 
   {
    if (collision.GetComponent<Player>() != null)
        myCamera.SetActive(true); 
   }

   private void OnTriggerExit2D(Collider2D collision) 
   {
    if (collision.GetComponent<Player>() != null)
        myCamera.SetActive(false); 
   }

   void OnDrawGizmos()
   {
    Gizmos.color = gizmosColor;
    Gizmos.DrawWireCube(cd.bounds.center, cd.bounds.size);
   }


}
