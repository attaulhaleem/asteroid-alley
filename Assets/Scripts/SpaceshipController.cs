using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 10.0f;
    public float acceleration = 6.5f;
    public float deceleration = 4.0f;
    public float tiltSpeed = 5.0f;
    public float maxTiltAngle = 15.0f;

    private Vector3 currentVelocity;
    private Quaternion baseRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentVelocity = Vector3.zero;
        baseRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        Vector3 targetVelocity = targetDirection * movementSpeed;
        float velocityChangeRate = (targetDirection.magnitude > 0.0f) ? acceleration : deceleration;
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * velocityChangeRate);
        transform.position += Time.deltaTime * currentVelocity;
        
        float tiltAngleX = currentVelocity.z / movementSpeed * maxTiltAngle;
        float tiltAngleZ = -currentVelocity.x / movementSpeed * maxTiltAngle;
        Quaternion tiltRotation = Quaternion.Euler(tiltAngleX, 0, tiltAngleZ);
        Quaternion targetRotation = baseRotation * tiltRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }
}