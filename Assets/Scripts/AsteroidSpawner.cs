using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject asteroidPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float minSpawnWidth = -150.0f;
    public float maxSpawnWidth = 150.0f;
    public float minSpawnHeight = 100.0f;
    public float maxSpawnHeight = 200.0f;
    public float minSpawnDistance = 200.0f;
    public float maxSpawnDistance = 200.0f;
    public float spawnWidthRange = 80.0f;
    public float spawnInterval = 0.1f;
    public float lifetime = 15.0f;

    [Header("Asteroid Dynamics")]
   public float minSpeed = 40.0f;
    public float maxSpeed = 100.0f;
    public float maxTumble = 3.0f;

    private float timer;

    void Update()
    {
        if (player == null || asteroidPrefab == null) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnAsteroid();
            timer = 0.0f;
        }
    }

    private void SpawnAsteroid()
    {
        float spawnX = player.position.x + Random.Range(minSpawnWidth, maxSpawnWidth);
        float spawnY = player.position.y + Random.Range(minSpawnHeight, maxSpawnHeight);
        float spawnZ = player.position.z + Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition = new(spawnX, spawnY, spawnZ);
        
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Random.rotation);

        if (asteroid.TryGetComponent<Rigidbody>(out var rb))
        {
            Vector3 targetOffset = new(Random.Range(-200.0f, 200.0f), Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f));
            Vector3 targetPosition = player.position + targetOffset;
            Vector3 directionToPlayer = (targetPosition - spawnPosition).normalized;
            float speed = Random.Range(minSpeed, maxSpeed);
            rb.linearVelocity = directionToPlayer * speed;
            rb.angularVelocity = Random.insideUnitSphere * maxTumble;
        }

        Destroy(asteroid, lifetime);
    }
}
