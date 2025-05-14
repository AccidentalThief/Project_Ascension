using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed = 10f; // Added rotation speed
    Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation; // Initialize with current rotation
    }

    void Update()
    {
        float x_move = Input.GetAxis("Horizontal");
        float z_move = Input.GetAxis("Vertical"); // Changed y_move to z_move for standard 3D movement

        Vector3 movementDirection = new Vector3(x_move, 0f, z_move).normalized; // Normalized to prevent faster diagonal movement

        if (movementDirection.magnitude > 0.01f) // Check if there is any movement
        {
            // Calculate the target rotation based on the movement direction and camera's Y-axis
            targetRotation = Quaternion.LookRotation(movementDirection);
            targetRotation *= Quaternion.Euler(0, cam.eulerAngles.y, 0); // Apply camera's Y rotation
        }

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Apply the movement
        transform.position += targetRotation * Vector3.forward * Time.deltaTime * moveSpeed * movementDirection.magnitude;
        // Using Vector3.forward because the rotation now aligns with the movement
        // Multiplying by movementDirection.magnitude ensures speed is consistent
    }
}