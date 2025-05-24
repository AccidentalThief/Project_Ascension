using UnityEngine;

public class FlyingAI : MonoBehaviour
{
    public Transform playerTarget;
    public float rotationSpeed = 20f;

    public float hoverRadius = 5.0f;
    public float followSpeed = 0.5f;

    public float hoverAmplitude = 0.2f;
    public float hoverFrequency = 1f;
    public float desiredHeightOffset = 1.5f;

    public float wobbleMagnitude = 0.5f;
    public float wobbleFrequency = 1f;
    public float distanceCorrectionThreshold = 0.5f;

    public float maxAngleDegreesFromHorizontal = 60f; // Max angle above the player's horizontal plane
    public float upwardCreepDiscourageBias = 0.1f; // Small positive value to bias wobble downwards

    private Vector3 _currentWobbleOffset;
    private float _nextWobbleTime;

    void Start()
    {
        if (playerTarget == null)
        {
            enabled = false;
            return;
        }

        _nextWobbleTime = Time.time;
    }

    void Update()
    {
        
        if (playerTarget == null) return;

        UpdateWobbleOffset();

        Quaternion targetRotation = Quaternion.LookRotation(playerTarget.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Vector3 currentPosition = transform.position;
        Vector3 playerPos = playerTarget.position;

        Vector3 toWisp = currentPosition - playerPos;
        float current3DDistance = toWisp.magnitude;
        Vector3 directionFromPlayer;

        if (current3DDistance < 0.01f)
        {
            directionFromPlayer = Random.onUnitSphere;
        }
        else
        {
            directionFromPlayer = toWisp.normalized;
        }

        Vector3 desiredSpherePoint;

        if (current3DDistance > hoverRadius + distanceCorrectionThreshold ||
            current3DDistance < hoverRadius - distanceCorrectionThreshold)
        {
            desiredSpherePoint = playerPos + directionFromPlayer * hoverRadius;
        }
        else
        {
            desiredSpherePoint = currentPosition;
        }

        float hoverBobbing = Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude + desiredHeightOffset;
        Vector3 finalTargetPosition = desiredSpherePoint + Vector3.up * hoverBobbing;

        // Apply angle cap (max 60 degrees above player's horizontal line at hoverRadius)
        float maxAllowedRelativeY = hoverRadius * Mathf.Sin(maxAngleDegreesFromHorizontal * Mathf.Deg2Rad);
        finalTargetPosition.y = Mathf.Min(finalTargetPosition.y, playerPos.y + maxAllowedRelativeY);
        
        finalTargetPosition += _currentWobbleOffset;

        transform.position = Vector3.Lerp(currentPosition, finalTargetPosition, followSpeed * Time.deltaTime);
    }

    private void UpdateWobbleOffset()
    {
        if (Time.time >= _nextWobbleTime)
        {
            Vector3 rawWobble = UnityEngine.Random.insideUnitSphere;
            
            // Bias the Y component downwards to discourage upward creep
            float biasedY = rawWobble.y - upwardCreepDiscourageBias;
            
            _currentWobbleOffset = new Vector3(rawWobble.x, biasedY, rawWobble.z) * wobbleMagnitude;
            _nextWobbleTime = Time.time + 1f / wobbleFrequency;
        }
    }
}