using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IMoveable
{
    private Camera mainCamera;

    [SerializeField] private float flySpeed = 10f;
    private float originalFlySpeed;
    public float FlySpeed => flySpeed;

    [SerializeField] private float rotationSpeed = 90f;

    public Vector2 CurrentVelocity => rb.linearVelocity;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalFlySpeed = flySpeed;
    }

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        CalculateMouseAngle();
        rb.linearVelocity = (Vector2)transform.right * flySpeed;
    }

    private void CalculateMouseAngle()
    {
        if (Mouse.current == null) return;

        Vector3 playerScreenPosition = mainCamera.WorldToScreenPoint(rb.position);

        Vector3 mousePosition = Mouse.current.position.ReadValue();

        // Check whether mouse is inside the screen
        if (mousePosition.x < 0 || mousePosition.x > Screen.width ||
            mousePosition.y < 0 || mousePosition.y > Screen.height)
            return;

        Vector3 direction = mousePosition - playerScreenPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float targetRotation = Mathf.MoveTowardsAngle(
            rb.rotation,
            angle,
            rotationSpeed * Time.fixedDeltaTime
        );

        rb.MoveRotation(targetRotation);
    }

    public void IncreaseFlySpeed(float amount)
    {
        flySpeed += amount;
    }

    public void DecreaseFlySpeed(float amount)
    {
        flySpeed -= amount;
    }

    public void RestoreOriginalFlySpeed()
    {
        flySpeed = originalFlySpeed;
    }
}


