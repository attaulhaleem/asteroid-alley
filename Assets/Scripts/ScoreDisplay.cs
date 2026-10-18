using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Awake()
    {
        // Automatically grab the TextMeshPro component on this object
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Read the score from the Singleton if it exists
        if (ScoreManager.Instance != null)
        {
            scoreText.text = "Score: " + ScoreManager.Instance.CurrentScore;
        }
    }
}