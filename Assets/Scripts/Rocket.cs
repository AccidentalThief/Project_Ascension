using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float speed;
    
    void Start()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit)) {
            transform.LookAt(hit.point);
        }
        else {
            Vector3 altTarget = ray.direction * 100;
            transform.LookAt(altTarget);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * Vector3.forward * Time.deltaTime * speed;
    }
}
