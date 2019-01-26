using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class HatMovement : MonoBehaviour
{
    private bool fly = false;
    private Vector3 targetPos;
    private Vector3 velocity = Vector3.zero;

    private const float animationTime = 2.5f;
    private readonly float smoothStep = 2.5f;

    private float time = 0.0f;

    private int thisDirection;

    private float startingScale;

    public bool finishedAnimation = false;

    public void Fly(Direction flyDirection)
    {
        startingScale = transform.localScale.x;

        thisDirection = flyDirection == Direction.RIGHT ? 1 : -1;

        GetComponent<Animation>().Play();
        targetPos = new Vector3(transform.position.x + thisDirection * 20, transform.position.y, 0);
        fly = true;
    }

    private void Update()
    {
        if(fly)
        {

            if (time < animationTime - 0.8f)
            {
                float boomerangEffect = (animationTime - 1.2f - Mathf.Max(animationTime - 1.2f, time)) * -thisDirection;
                Vector3 boomerangVector = new Vector3(boomerangEffect * 300, 0, 0);
                transform.position = Vector3.SmoothDamp(transform.position, targetPos - boomerangVector, ref velocity, smoothStep);

                float endTransform = startingScale * (1.2f / Mathf.Pow(Mathf.Max(1.2f, time), 1.5f));

                transform.localScale = new Vector3(endTransform, endTransform, 1f);
            }

            else if (time > animationTime)
            {
                fly = false;
                // transform.localScale = new Vector3(startingScale, startingScale, 1f);
                finishedAnimation = true;
            }

            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if(fly)
        {
            time += Time.fixedDeltaTime;
        }
    }
}
