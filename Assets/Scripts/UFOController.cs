using UnityEngine;

public class UFOHover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 10.0f;
    public float acceleration = 6.5f;
    public float deceleration = 4.0f;
    public float spinSpeed = 30.0f;
    public float hoverSpeed = 1.5f;
    public float maxHoverOffset = 0.3f;
    public float flutterSpeed = 1.0f;
    public float maxFlutterOffset = 0.05f;
    public float tiltSpeed = 5.0f;
    public float maxTiltAngle = 15.0f;

    private float hoverHeight;
    private float spinAngleY;
    private Vector3 logicalPosition;
    private Vector3 currentVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hoverHeight = transform.position.y;
        spinAngleY = transform.eulerAngles.y;
        logicalPosition = transform.position;
        currentVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        Vector3 targetVelocity = targetDirection * movementSpeed;
        float velocityChangeRate = (targetDirection.magnitude > 0.0f) ? acceleration : deceleration;
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * velocityChangeRate);
        logicalPosition += Time.deltaTime * currentVelocity;

        float hoverOffset = maxHoverOffset * Mathf.Sin(Time.time * hoverSpeed);

        float flutterOffsetX = maxFlutterOffset * (2.0f * (Mathf.PerlinNoise(Time.time * flutterSpeed, 12.34f) - 0.5f));
        float flutterOffsetY = maxFlutterOffset * (2.0f * (Mathf.PerlinNoise(Time.time * flutterSpeed, 56.78f) - 0.5f));
        float flutterOffsetZ = maxFlutterOffset * (2.0f * (Mathf.PerlinNoise(Time.time * flutterSpeed, 90.12f) - 0.5f));

        transform.position = new Vector3(logicalPosition.x + flutterOffsetX, hoverHeight + hoverOffset + flutterOffsetY, logicalPosition.z + flutterOffsetZ);
        
        spinAngleY += Time.deltaTime * spinSpeed;
        Quaternion spinRotation = Quaternion.Euler(0, spinAngleY, 0);
        
        float tiltAngleX = currentVelocity.z / movementSpeed * maxTiltAngle;
        float tiltAngleZ = -currentVelocity.x / movementSpeed * maxTiltAngle;
        Quaternion tiltRotation = Quaternion.Euler(tiltAngleX, 0, tiltAngleZ);

        Quaternion targetRotation = tiltRotation * spinRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }
}