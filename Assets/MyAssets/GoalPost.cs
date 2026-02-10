using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPost : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject winPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if player hit the goal
        if (collision.GetComponent<PlayerController>() != null)
        {
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        Debug.Log("LEVEL COMPLETE!");

        // 1. Show the UI
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        // 2. Pause the Game (Physics & Time stop)
        Time.timeScale = 0f;
    }

    // Call this function from your UI Button
    public void RestartLevel()
    {
        // Unpause time before reloading
        Time.timeScale = 1f;

        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}