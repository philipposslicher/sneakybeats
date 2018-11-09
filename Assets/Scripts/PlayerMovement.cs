using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float xMovement;
    private float yMovement;
    
    public float moveSpeed;
    
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    
    void FixedUpdate()
    {
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");
        
        rb.velocity = new Vector2 (xMovement * moveSpeed, yMovement * moveSpeed);
    }
}
