using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed = 10f;
    Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        float x_move = Input.GetAxis("Horizontal");
        float z_move = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(x_move, 0f, z_move).normalized;

        if (movementDirection.magnitude > 0.01f)
        {
            targetRotation = Quaternion.LookRotation(movementDirection);
            targetRotation *= Quaternion.Euler(0, cam.eulerAngles.y, 0);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position += targetRotation * Vector3.forward * Time.deltaTime * moveSpeed * movementDirection.magnitude;
    }
}