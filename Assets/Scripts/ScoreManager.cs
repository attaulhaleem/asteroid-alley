using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // The Singleton instance
    public static ScoreManager Instance { get; private set; }

    [Header("References")]
    public Transform player;

    [Header("Settings")]
    public float scoreMultiplier = 1.0f;

    // Public property so other scripts can read the score safely
    public int CurrentScore { get; private set; } 

    private Vector3 startPosition;
    private float highestDistance = 0f;

    void Awake()
    {
        // Initialize the Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (player != null)
        {
            startPosition = player.position;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calculate straight-line distance from where the player started
        float distanceTraveled = Vector3.Distance(startPosition, player.position);

        // Ensure the score only goes up
        if (distanceTraveled > highestDistance)
        {
            highestDistance = distanceTraveled;
        }
        
        // Calculate and store the score globally
        CurrentScore = Mathf.FloorToInt(highestDistance * scoreMultiplier);
    }
}