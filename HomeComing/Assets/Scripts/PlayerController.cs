using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum Direction { RIGHT, LEFT };

    public float _speed = 5f;
    public float _jumpingForce = 7f;

    private Direction _direction;

    private bool _isGrounded = false;
    public bool IsGrounded { get { return this._isGrounded; } }
    private float _groundSlopeThreshold = 0.5f;
    private GameObject currentGround = null;

    private AudioManager audioManager;
    private Rigidbody2D rb;
    private Animation anim;
    private SpriteRenderer graphicsSprite;

    private GameObject hatClone;
    public GameObject hat;

    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animation>();
        graphicsSprite = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _direction = horizontal > 0 ? Direction.RIGHT : (horizontal < 0 ? Direction.LEFT : _direction);
        bool jump = Input.GetKeyDown(KeyCode.UpArrow);
        bool attack = Input.GetKeyDown(KeyCode.Z);

        float xVel = horizontal * _speed;
        float yVel = rb.velocity.y;

        graphicsSprite.flipX = _direction == Direction.RIGHT ? false : true;

        if (_isGrounded)
        {
            yVel = jump ? _jumpingForce : rb.velocity.y;

            if (horizontal != 0)
            {
                anim.Play("HomieWalkRight");

                if(!audioManager.isPlaying())
                    audioManager.Play("walking");
            }
        }
        if (attack && !hatClone)
            Attack();

        if (hatClone && hatClone.GetComponent<HatMovement>().finishedAnimation)
        {
            Destroy(hatClone.gameObject);

            hat.GetComponent<SpriteRenderer>().enabled = true;
        }

        rb.velocity = new Vector2(xVel, yVel);
    }

    private void Attack()
    {
        hatClone = Instantiate(hat.gameObject);
        hatClone.transform.position = hat.transform.position;
        hat.GetComponent<SpriteRenderer>().enabled = false;

        hatClone.transform.SetParent(null);
        hatClone.GetComponent<HatMovement>().Fly(_direction);
        audioManager.Play("hatThrow");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > _groundSlopeThreshold)
            {
                currentGround = collision.gameObject;
                _isGrounded = true;
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentGround)
        {
            currentGround = null;
            _isGrounded = false;
        }
    }
}
