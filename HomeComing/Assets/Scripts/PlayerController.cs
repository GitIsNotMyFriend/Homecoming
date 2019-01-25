using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpingForce = 7f;
    public bool isGrounded = false;
    private GameObject currentGround = null;
    private float groundAngleThreshold = 0.5f;

    private Rigidbody2D rb;
    private Animation anim;
    private SpriteRenderer graphicsSprite;
    public GameObject hat;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animation>();
        graphicsSprite = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool jump = Input.GetKeyDown(KeyCode.UpArrow);
        bool attack = Input.GetKeyDown(KeyCode.Z);

        rb.velocity = new Vector2(horizontal * speed, 
            jump ? jumpingForce : rb.velocity.y);


        if (horizontal > 0)
            graphicsSprite.flipX = false;
        if (horizontal < 0)
            graphicsSprite.flipX = true;

        if (isGrounded)
        {
            //if (jump)
            //    anim.Play("HomieJump");
            //if (rb.velocity.y < 0)
            //    anim.Play("HomieGroundFall");
            if (horizontal != 0)
                anim.Play("HomieWalkRight");
        }
        if (attack)
            Attack(horizontal);
    }

    private void Attack(float direction)
    {
        anim.Play();
        hat.transform.SetParent(null);
        hat.GetComponent<HatMovement>().Fly((int)direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > groundAngleThreshold)
            {
                currentGround = collision.gameObject;
                isGrounded = true;
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentGround)
        {
            currentGround = null;
            isGrounded = false;
        }
    }
}
