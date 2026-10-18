using UnityEngine;
using TMPro; // Required for TextMeshPro

public class ScoreController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public TextMeshProUGUI scoreText; 

    [Header("Settings")]
    public float scoreMultiplier = 1.0f;

    private Vector3 startPosition;
    private float highestDistance = 0f;

    void Start()
    {
        if (player != null)
        {
            startPosition = player.position;
        }
    }

    void Update()
    {
        if (player == null || scoreText == null) return;

        // Calculate straight-line distance from where the player started
        float distanceTraveled = Vector3.Distance(startPosition, player.position);

        // Ensure the score only goes up, even if the player gets knocked backward
        if (distanceTraveled > highestDistance)
        {
            highestDistance = distanceTraveled;
        }
        
        // Apply multiplier and convert to a whole number
        int score = Mathf.FloorToInt(highestDistance * scoreMultiplier);

        scoreText.text = "Score: " + score;
    }
}