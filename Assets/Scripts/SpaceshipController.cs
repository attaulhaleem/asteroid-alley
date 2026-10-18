using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceshipController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 10.0f;
    public float acceleration = 6.5f;
    public float deceleration = 4.0f;
    public float tiltSpeed = 5.0f;
    public float maxTiltAngle = 15.0f;
    
    [Header("Boost Settings")]
    public float boostForce = 500.0f;

    private Vector3 currentVelocity;
    private Quaternion baseRotation;
    private Rigidbody rb;

    void Start()
    {
        currentVelocity = Vector3.zero;
        baseRotation = transform.rotation; 
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Do not allow reversing
        float forwardInput = Mathf.Clamp01(Input.GetAxis("Vertical")); 
        Vector3 targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0, forwardInput).normalized;
        Vector3 targetVelocity = targetDirection * movementSpeed;
        float velocityChangeRate = (targetDirection.magnitude > 0.0f) ? acceleration : deceleration;
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * velocityChangeRate);
        transform.Translate(currentVelocity * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && rb != null)
        {
            rb.AddForce(transform.forward * boostForce, ForceMode.Impulse);
        }

        float tiltAngleX = currentVelocity.z / movementSpeed * maxTiltAngle;
        float tiltAngleZ = -currentVelocity.x / movementSpeed * maxTiltAngle;
        Quaternion tiltRotation = Quaternion.Euler(tiltAngleX, 0, tiltAngleZ);
        Quaternion targetRotation = baseRotation * tiltRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Freezes the game immediately upon physically hitting an asteroid
        Debug.Log("Ship crashed into " + collision.gameObject.name);
        Time.timeScale = 0.0f;
    }
}