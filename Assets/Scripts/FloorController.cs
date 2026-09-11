using UnityEngine;

public class FloorController : MonoBehaviour
{
    public Transform player;
    public float snapSize = 10.0f;
    public float floorHeight = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            Debug.LogWarning("Player is not assigned to endless floor!");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        float snappedX = Mathf.Round(player.position.x / snapSize) * snapSize;
        float snappedZ = Mathf.Round(player.position.z / snapSize) * snapSize;
        transform.position = new Vector3(snappedX, floorHeight, snappedZ);
    }
}
