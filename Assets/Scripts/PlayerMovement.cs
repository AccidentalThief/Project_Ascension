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

        // if we move or shoot

        if (movementDirection.magnitude > 0.01f || Input.GetMouseButton(0))
        {
            // LookRotation returns an error if looking towards (0, 0, 0)
            if (Input.GetMouseButton(0))
                targetRotation = Quaternion.LookRotation(new Vector3(0f, 0f, 1f)); 
            else
                targetRotation = Quaternion.LookRotation(movementDirection);
            targetRotation *= Quaternion.Euler(0, cam.eulerAngles.y, 0);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Input.GetMouseButton(0))
            transform.position += targetRotation * movementDirection * Time.deltaTime * moveSpeed;
        else
            transform.position += targetRotation * Vector3.forward * Time.deltaTime * moveSpeed * movementDirection.magnitude;
    }
}