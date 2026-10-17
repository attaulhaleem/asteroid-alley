using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float rotationSmoothSpeed = 5.0f;
    private Quaternion rotationOffset;
    private Vector3 positionOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target is not assigned!");
            return;
        }
        
        positionOffset = transform.position - target.position;
        rotationOffset = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        transform.position = target.position + positionOffset;
        // Smoothly tilt the camera when the spaceship moves
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationOffset, Time.deltaTime * rotationSmoothSpeed);
    }
}
