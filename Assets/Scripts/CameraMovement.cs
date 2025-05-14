using UnityEngine;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 followDistance;
    [SerializeField] float mouseSensitivity = 2f;

    float rotationY;
    //float rotationX;

    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;

        var targetRotation = Quaternion.Euler(16.89f, rotationY, 0f);
        
        transform.position = target.position + targetRotation * followDistance;
        transform.rotation = targetRotation;
    }
}
