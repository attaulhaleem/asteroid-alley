using UnityEngine;

public class AsteroidCleanup : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.Find("Spaceship").transform;
    }

    void Update()
    {
        if (player == null) return;

        // If the asteroid is ever physically behind the player on the Z-axis, destroy it instantly
        if (transform.position.z < player.position.z - 10.0f)
        {
            Destroy(gameObject);
        }
    }
}