using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerBehaviour : MonoBehaviour
{
    [Header("Movement")]
    public float horizontalForce;
    public float verticalForce;

    public bool isGrounded;
    public Transform groundOrigin;
    public float groundRadius;
    public LayerMask groundLayerMask;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
    }

    private void Move()
    {
        if (isGrounded)
        {
            // float deltaTime = Time.deltaTime;

            // Keyboard input 
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            float jump = Input.GetAxisRaw("Jump");

            // Check for flip
            if (x != 0)
                x = FlipAnimation(x);

            // Touch input 
            Vector2 worldTouch = new Vector2();
            foreach (var touch in Input.touches)
            {
                worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }

            float horizontalMoveForce = x * horizontalForce;
            float jumpMoveForce = jump * verticalForce;

            float mass = rb.mass * rb.gravityScale;

            rb.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);
            rb.velocity *= 0.99f;   // Scaling factor
        }


        //rb.velocity *= 0.99f;
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down, 0, groundLayerMask);

        isGrounded = (hit) ? true : false;
    }



    private float FlipAnimation(float x)
    {
        // depending on direction, scale across the x acis 
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);

        return x; 
    }

    // UTILITIES

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundOrigin.position, groundRadius);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    isGrounded = true;
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    isGrounded = false;
    //}
}
