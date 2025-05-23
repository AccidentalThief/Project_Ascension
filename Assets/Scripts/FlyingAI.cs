using UnityEngine;

public class FlyingAI : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 0.5f;
    public float hoverAmplitude = 0.2f;
    public float hoverFrequency = 1f;
    public float desiredHeightOffset = 1.5f;
    public float wobbleMagnitude = 0.5f; // How far it can wobble
    public float wobbleFrequency = 1f;   // How often it picks a new wobble target

    private Vector3 currentWobbleOffset = Vector3.zero;
    private float nextWobbleTime = 0f;

void Update() {
    if (target != null) {
        Vector3 currentPosition = transform.position;
        Vector3 targetBasePosition = target.position;

        // Update wobble offset
        if (Time.time >= nextWobbleTime) {
            currentWobbleOffset = Random.insideUnitSphere * wobbleMagnitude;
            nextWobbleTime = Time.time + 1f / wobbleFrequency; // Calculate next time
        }

        // Calculate the desired XZ position with wobble
        Vector3 desiredXZ = Vector3.Lerp(new Vector3(currentPosition.x, 0, currentPosition.z),
                                        new Vector3(targetBasePosition.x + currentWobbleOffset.x, 0, targetBasePosition.z + currentWobbleOffset.z),
                                        followSpeed * Time.deltaTime);

        // Calculate the desired Y position with hover
        float hoverY = Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        float desiredY = targetBasePosition.y + desiredHeightOffset + hoverY + currentWobbleOffset.y; // Add wobble to Y as well

        transform.position = new Vector3(desiredXZ.x, desiredY, desiredXZ.z);
    }
}
}
