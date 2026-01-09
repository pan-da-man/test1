using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player GameObject and PlayerController script.
    public GameObject player;
    private PlayerController playerController;

    // New position for the camera when count equals 1
    public Vector3 newCameraPosition = new Vector3(45, 15, 0);

    // Start is called before the first frame update.
    void Start()
    {
        // Get the PlayerController component from the player GameObject.
        playerController = player.GetComponent<PlayerController>();

        // Ensure playerController is properly assigned
        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on player.");
        }
    }

    // LateUpdate is called once per frame after all Update functions have been completed.
    void LateUpdate()
    {
        // Check if count equals 1
        if (playerController != null && playerController.count == 1)
        {
            // Change the camera's position to the new specified position
            transform.position = newCameraPosition;

            // Display "Good job" in the console
            Debug.Log("Good job");

            // Optionally, prevent further checks after the first time
            playerController.count = -1; // Prevent repeated message
        }
    }
}
