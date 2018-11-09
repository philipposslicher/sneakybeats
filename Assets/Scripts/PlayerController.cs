using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    // private bool isPlayingWalkSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

    }

    private void FixedUpdate()
    {
        rb.AddForce(moveVelocity, ForceMode2D.Force);
    }
}
