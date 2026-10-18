using UnityEngine;

public class AsteroidCleanup : MonoBehaviour
{
    private GameObject playerObject;

    void Start()
    {
        playerObject = GameObject.Find("Spaceship");
    }

    void Update()
    {
        if (playerObject == null) return;

        if (transform.position.z < playerObject.transform.position.z - 10.0f)
        {
            Destroy(gameObject);
        }
    }
}