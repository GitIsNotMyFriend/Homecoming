using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Camera mainCamera;

    private float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    public float cloudParallax = 30;

    public Sprite backgroundImage;
    public Transform cloudLayer;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (target)
        {
            Vector3 point = mainCamera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;

            Vector2 cameraProjection = GetCameraProjectionSize();
            Vector2 backgroundSize = backgroundImage.bounds.max;

            Vector2 cameraBound = backgroundSize - (cameraProjection / 2);

            destination.x = Mathf.Clamp(destination.x, -cameraBound.x, cameraBound.x);
            destination.y = Mathf.Clamp(destination.y, -cameraBound.y, cameraBound.y);
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

            cloudLayer.position = new Vector3((destination.x - Vector3.zero.x) / cloudParallax, 0, 0);
        }

    }

    private Vector2 GetCameraProjectionSize()
    {
        Vector2 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        Vector2 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        return new Vector2(bottomRight.x - topLeft.x, bottomRight.y - topLeft.y);
    }
}
