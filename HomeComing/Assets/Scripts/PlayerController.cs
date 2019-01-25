using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpingForce;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool jump = Input.GetKeyDown(KeyCode.Space);

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
}
