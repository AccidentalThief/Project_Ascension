using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float moveSpeed;
    float rotationY;

    // Update is called once per frame
    void Update()
    {
        float x_move = Input.GetAxis("Horizontal");
        float y_move = Input.GetAxis("Vertical");

        Vector3 move = (new Vector3(x_move, 0f, y_move)).normalized;

        transform.position += move * Time.deltaTime * moveSpeed;

        //if (Input.anyKey) {
            rotationY = cam.rotation.y;
        
            Quaternion targetRotation = Quaternion.Euler(0, rotationY, 0);
            transform.rotation = targetRotation;
        //}
    }
}
