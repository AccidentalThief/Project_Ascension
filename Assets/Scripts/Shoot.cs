using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] float timeBetweenShots = .5f;
    float timer = 0f;
    int shots = 0;
    
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0)) {
            if (timer >= timeBetweenShots || shots == 0) {
                timer = 0f;
                shots++;
                ShootRocket();
            }
        }
    }

    void ShootRocket() {
        GameObject rocket = rocketPrefab;
        rocket.transform.position = transform.position;
        rocket.transform.rotation = transform.rotation;
        Instantiate(rocket);
    }
}
