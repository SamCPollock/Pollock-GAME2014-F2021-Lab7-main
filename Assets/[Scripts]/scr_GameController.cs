using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_GameController : MonoBehaviour
{

    public Transform player;
    public Transform playerSpawnPoint;


    void Start()
    {
        player.position = playerSpawnPoint.position;    
    }

}
