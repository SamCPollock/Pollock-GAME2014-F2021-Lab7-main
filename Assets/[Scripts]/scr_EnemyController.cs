using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float runForce;
    public Transform lookAheadPoint;
    public Transform lookInFrontPoint;
    public LayerMask groundLayerMask;
    public LayerMask obstacleLayerMask;
    public bool isGroundAhead;

    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        LookAhead();
        LookInFront();
        MoveEnemy();
    }
    
    private void LookAhead()
    {
        var hit = Physics2D.Linecast(transform.position, lookAheadPoint.position, groundLayerMask);

        if (hit)
        {
            isGroundAhead = true;
        }
        else
        {
            isGroundAhead = false;
        }
    }

    private void LookInFront()
    {
        var hit = Physics2D.Linecast(transform.position, lookInFrontPoint.position, obstacleLayerMask);

        if (hit)
        {
            Flip();
        }
        else
        {
        }
    }

    private void MoveEnemy()
    {
        if (isGroundAhead)
        {
            rb.AddForce(Vector2.left * runForce * transform.localScale.x);

            rb.velocity *= -0.9f;
        }
        else
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, lookAheadPoint.position);
        Gizmos.DrawLine(transform.position, lookInFrontPoint.position);

    }
}
