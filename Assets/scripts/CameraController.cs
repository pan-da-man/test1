using UnityEngine;
using UnityEngine.InputSystem; // For new Input System

public class StableFPSCameraFollow : MonoBehaviour
{
    [Header("References")]
    public GameObject player;                   // The rolling ball
    private PlayerController playerController;

    [Header("Count 1 Settings")]
    public Vector3 cameraPositionCount1 = new Vector3(45, 15, 0); // Top-down
    public Vector3 cameraRotationCount1 = new Vector3(90, 0, 0);
    private bool movedForCount1 = false;

    [Header("Count 2 Settings")]
    public Vector3 firstPersonOffset = new Vector3(0, 2.5f, 0);  // Camera slightly above ball
    public float mouseSensitivity = 100f;
    private bool firstPersonEnabled = false;

    [Header("Count 3 Settings")]
    public Vector3 lastCameraPosition = new Vector3(0, 15, 0);  // Teleport position
    private bool lastCameraEnabled = false;

    // FPS rotation state
    private float pitch = 0f;
    private float yaw = 0f;
    public UIManager uiManager;
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference not assigned!");
            return;
        }

        playerController = player.GetComponent<PlayerController>();
        if (playerController == null)
            Debug.LogError("PlayerController component not found on player!");
    }

    void LateUpdate()
{
    if (playerController == null || player == null)
        return;

    // -------- Count == 1: top-down view ----------
    if (playerController.count == 1 && !movedForCount1)
    {
        transform.position = cameraPositionCount1;
        transform.rotation = Quaternion.Euler(cameraRotationCount1);
        movedForCount1 = true;
        firstPersonEnabled = false;
        lastCameraEnabled = false;

        if (uiManager != null)
            uiManager.IncrementCount();
        else
            Debug.LogWarning("UIManager reference is not set!");

        Debug.Log("Camera moved for count 1!");
    }

    // -------- Count == 2: first-person view ----------
    if (playerController.count == 2 && !firstPersonEnabled)
    {
        firstPersonEnabled = true;
        movedForCount1 = false;
        lastCameraEnabled = false;

        // Stop parenting so camera does not roll with ball
        transform.SetParent(null);

        // Initialize FPS rotation based on ball's forward
        yaw = player.transform.eulerAngles.y;
        pitch = 0f;

        // Lock cursor for FPS
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (uiManager != null)
            uiManager.IncrementCount();
        else
            Debug.LogWarning("UIManager reference is not set!");

        Debug.Log("First-person camera enabled!");
    }

    // Handle FPS mouse look
    if (firstPersonEnabled)
    {
        HandleMouseLook();

        // Follow the ball at a small vertical offset
        transform.position = player.transform.position + firstPersonOffset;
    }

    // -------- Count == 3: last camera with X rotation 90 ----------
    if (playerController.count == 3 && !lastCameraEnabled)
    {
        lastCameraEnabled = true;
        firstPersonEnabled = false;
        movedForCount1 = false;

        transform.position = lastCameraPosition;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f); // X rotation = 90

        // Unlock cursor if it was locked
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (uiManager != null)
            uiManager.IncrementCount();
        else
            Debug.LogWarning("UIManager reference is not set!");

        Debug.Log("Last camera enabled with X rotation 90!");
    }
}


// Rotate camera based on mouse movement
private void HandleMouseLook()
    {
        if (Mouse.current == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * mouseSensitivity;
        pitch -= mouseDelta.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
