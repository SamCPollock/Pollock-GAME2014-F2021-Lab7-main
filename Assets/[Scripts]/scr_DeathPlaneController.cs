using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DeathPlaneController : MonoBehaviour
{
    public Transform playerSpawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = playerSpawnPoint.position;
        }
        else
        {
            collision.gameObject.SetActive(false);
        }
    }
}
