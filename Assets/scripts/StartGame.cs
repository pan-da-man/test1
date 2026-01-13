using UnityEngine;

public class StartButtonHandler : MonoBehaviour
{
    // Assign your Intro Panel in the Inspector
    public GameObject introPanel;

    // This function is called when the button is pressed
    public void OnStartButtonPressed()
    {
        // Hide the intro panel
        if (introPanel != null)
            introPanel.SetActive(false);

        // Hide the button itself
        gameObject.SetActive(false);
    }
}
