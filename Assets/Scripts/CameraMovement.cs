using UnityEngine;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 followDistance;
    [SerializeField] float mouseSensitivity = 2f;

    float rotationY;
    float rotationX = 16.9f;
    //float rotationX;

    void Start() {
        followDistance = followDistance - target.position;
    }

    void Update()
    {
        rotationX += Input.GetAxis("Mouse Y") * -mouseSensitivity;
        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;

        if (rotationX > 360)
            rotationX -= 360;
        if (rotationY > 360)
            rotationY -= 360;

        if (rotationX > 60f)
            rotationX = 60f;
        if (rotationX < -60f)
            rotationX = -60f;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        
        transform.position = target.position + targetRotation * followDistance;
        transform.rotation = targetRotation;
    }
}
