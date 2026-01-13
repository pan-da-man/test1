using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerController playerController; // Assign in Inspector or find in Start
    public GameObject introPanel;
    public GameObject startButton;
    public GameObject gameOverPanel;
    public GameObject gameOverText;
    public GameObject gameOverText2;

    private int count = 0;

    void Start()
    {
        if (playerController == null)
        {
            playerController = GameObject.FindObjectOfType<PlayerController>();
            if (playerController == null)
                Debug.LogError("UIManager: PlayerController not found!");
        }

        // Hide Game Over UI at start
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameOverText != null) gameOverText.SetActive(false);
        if (gameOverText2 != null) gameOverText2.SetActive(false);
    }

    public void OnStartButtonPressed()
    {
        if (introPanel != null) introPanel.SetActive(false);
        if (startButton != null) startButton.SetActive(false);
    }

    public void IncrementCount()
    {
        count++;
        Debug.Log("Count: " + count);

        if (playerController != null && playerController.count == 3)
        {
            ShowGameOver();
            Debug.Log("Count = 3, showing Game Over!");
        }
    }

    private void ShowGameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverText != null) gameOverText.SetActive(true);
        if (gameOverText2 != null) gameOverText2.SetActive(true);
       
    }
}
