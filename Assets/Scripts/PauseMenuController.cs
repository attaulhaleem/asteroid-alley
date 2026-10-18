using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenu;
    public GameObject mainMenuUI;
    public GameObject gameOverMenuUI; // Add this reference

    private bool isPaused = false;

    void Start()
    {
        if (pauseMenu != null) pauseMenu.SetActive(false);
    }

    void Update()
    {
        // Ignore the Escape key completely if the Main Menu or Game Over Menu is visible
        bool isMainMenuActive = mainMenuUI != null && mainMenuUI.activeInHierarchy;
        bool isGameOverActive = gameOverMenuUI != null && gameOverMenuUI.activeInHierarchy;

        if (isMainMenuActive || isGameOverActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void OnClickResumeButton()
    {
        ResumeGame();
    }

    public void OnClickMainMenuButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}