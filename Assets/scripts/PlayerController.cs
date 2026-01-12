using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public int count;

    private float movementX;
    private float movementY;

    public float speed = 5f;

    public delegate void CountChanged(int newCount);
    public event CountChanged OnCountChanged;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        if (count < 2)
        {
            // Normal movement using input
            Vector3 movement = new Vector3(movementX, 0f, movementY);
            rb.AddForce(movement * speed);
        }
        else
        {
            // Movement towards the mouse cursor
            MoveTowardsCursor();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            OnCountChanged?.Invoke(count);
        }
    }

    private void MoveTowardsCursor()
    {
        // Raycast from camera to mouse position
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Horizontal plane at y=0

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 direction = (targetPoint - transform.position).normalized;

            rb.AddForce(direction * speed);
        }
    }
}
