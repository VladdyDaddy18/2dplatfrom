using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public static Player_Manager instance;

    [SerializeField] public Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;
                     public GameObject currentPlayer;


    public void Awake()
    {
        instance = this;
        

    }

    

    public void PlayerRespawn()
    {
        if (currentPlayer == null)
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
    }


}
