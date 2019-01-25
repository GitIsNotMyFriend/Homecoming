using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatMovement : MonoBehaviour
{
    private bool fly = false;
    private Vector3 targetPos;
    private Vector3 velocity = Vector3.zero;
    private float time = 5f;

    public void Fly(int direction)
    {
        direction = direction < 0 ? -1 : 1;

        targetPos = new Vector3(transform.position.x + direction * 100, 0, transform.position.y);
        fly = true;
    }

    private void Update()
    {
        if(fly)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, time);
        }
    }
}
