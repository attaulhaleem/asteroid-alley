using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject mainMenu;
    public GameObject gameUI;

    void Start()
    {
        // Pause the game world in the background while on the menu
        Time.timeScale = 0.0f;

        // Show main menu, hide game UI
        if (mainMenu != null) mainMenu.SetActive(true);
        if (gameUI != null) gameUI.SetActive(false);
    }

    public void OnClickStartButton()
    {
        // Hide main menu, show game UI
        if (mainMenu != null) mainMenu.SetActive(false);
        if (gameUI != null) gameUI.SetActive(true);

        // Resume time to start playing
        Time.timeScale = 1.0f;
    }

    public void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}