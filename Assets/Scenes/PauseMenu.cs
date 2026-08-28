using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public Movement playerMovement;
    public PlayerInteraction playerInteraction;

    private bool isPaused = false;

    private bool movementWasEnabled;
    private bool interactionWasLocked;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;

        movementWasEnabled = playerMovement.enabled;
        interactionWasLocked = playerInteraction.interactionLocked;

        playerMovement.enabled = false;
        playerInteraction.interactionLocked = true;

        pauseMenuUI.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;

        // Pause ALL audio
        AudioListener.pause = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;

        pauseMenuUI.SetActive(false);

        // Resume the game
        Time.timeScale = 1f;

        // Resume ALL audio
        AudioListener.pause = false;

        playerMovement.enabled = movementWasEnabled;
        playerInteraction.interactionLocked = interactionWasLocked;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        // IMPORTANT: make sure audio isn't left paused
        AudioListener.pause = false;

        GameEndingState.returningFromBody = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

        // Reset audio before quitting
        AudioListener.pause = false;

        Debug.Log("Quit Game");
        Application.Quit();
    }
}