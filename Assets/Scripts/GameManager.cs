using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static readonly WaitForSecondsRealtime _waitForSecondsRealtime2_0 = new(2.0f);

    // Singleton instance allows other scripts to easily find the GameManager
    public static GameManager Instance;

    [Header("UI References")]
    public GameObject gameOverMenu;

    [Header("Explosion Effect")]
    public GameObject explosionPrefab;
    public GameObject asteroidPrefab; 
    public int debrisCount = 8; 

    private void Awake()
    {
        // Set up the Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(false);
        }
    }

    public void GameOver(Vector3 impactPoint)
    {
        // Turn off spawner so no new rocks appear
        if (TryGetComponent<AsteroidSpawner>(out var spawner))
        {
            spawner.enabled = false;
        }

        GameObject explosion = Instantiate(explosionPrefab, impactPoint, Quaternion.identity);
        if (explosion.TryGetComponent<ParticleSystem>(out var ps))
        {
            // Shrink explosion particles over time
            var sizeModule = ps.sizeOverLifetime;
            sizeModule.enabled = true;
            sizeModule.size = new ParticleSystem.MinMaxCurve(1.0f, AnimationCurve.Linear(0f, 1f, 1f, 0f));
        }

        CreateDebris(impactPoint);
        
        // Drop into dramatic slow-motion
        Time.timeScale = 0.2f; 

        // Show Game Over screen after watching the crash
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        // Wait for real time seconds
        yield return _waitForSecondsRealtime2_0;

        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(true);
        }
        
        // Now freeze the game completely
        Time.timeScale = 0.0f;
    }

    private void CreateDebris(Vector3 impactPoint)
    {
        for (int i = 0; i < debrisCount; i++)
        {
            GameObject debris = Instantiate(asteroidPrefab, impactPoint, Random.rotation);
            float shardX = Random.Range(0.05f, 0.4f);
            float shardY = Random.Range(0.05f, 0.1f);
            float shardZ = Random.Range(0.1f, 0.5f);
            debris.transform.localScale = new Vector3(shardX, shardY, shardZ);

            // Remove trails from debris
            if (debris.TryGetComponent<TrailRenderer>(out var trail))
            {
                Destroy(trail);
            }

            if (debris.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.linearVelocity = Random.onUnitSphere * Random.Range(20f, 60f);
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}