using UnityEngine;

public class TeleportOnCount : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerController playerController;

    // Track which counts have already triggered teleport
    private bool teleportedCount1 = false;
    private bool teleportedCount2 = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.OnCountChanged += HandleCountChanged;
        }
    }

    void HandleCountChanged(int newCount)
    {
        // Teleport for count == 1
        if (newCount == 1 && !teleportedCount1)
        {
            TeleportTo(new Vector3(57.48f, 0.5f, -3.14f));
            teleportedCount1 = true;
        }
        //104.75f, 0.5f, -9f
        // Teleport for count == 2
        if (newCount == 2 && !teleportedCount2)
        {
            TeleportTo(new Vector3(86.7f, 1.1f, 8f));
            teleportedCount2 = true;
        }
    }

    private void TeleportTo(Vector3 targetPosition)
    {
        // Stop current physics movement
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Teleport
        rb.position = targetPosition;
    }

    void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.OnCountChanged -= HandleCountChanged;
        }
    }
}
