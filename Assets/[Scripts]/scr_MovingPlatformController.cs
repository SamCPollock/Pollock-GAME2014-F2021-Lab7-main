using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_MovingPlatformController : MonoBehaviour
{
    [Header("Movement")]
    public scr_MovingPlatformStates direction;
        
    [Range(.1f, 10f)]
    public float speed;
    [Range(1, 20)]
    public float distance;
    [Range(0.05f, 0.1f)]
    public float distanceOffset; 
    public bool isLooping;

    private bool isMoving;


    private Vector2 startingPos;


    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
        if (isLooping)
        {
            isMoving = true;
        }
    }

    private void MovePlatform()
    {
        float pingPongValue = (isMoving) ? Mathf.PingPong(Time.time * speed, distance) : distance;

        if ((!isLooping) && (pingPongValue >= distance - distanceOffset))
        {
            isMoving = false;
        }
        

        switch(direction)
        {
            case scr_MovingPlatformStates.HORIZONTAL:
                transform.position = new Vector2(startingPos.x + pingPongValue, transform.position.y);
                break;
            case scr_MovingPlatformStates.VERTICAL:
                transform.position = new Vector2(transform.position.x, startingPos.y + pingPongValue);
                break;
            case scr_MovingPlatformStates.DIAGONAL_UP:
                transform.position = new Vector2(startingPos.x + pingPongValue, startingPos.y + pingPongValue);
                break;
            case scr_MovingPlatformStates.DIAGONAL_DOWN:
                transform.position = new Vector2(startingPos.x - pingPongValue, startingPos.y - pingPongValue);
                break;
        }
    }
}
