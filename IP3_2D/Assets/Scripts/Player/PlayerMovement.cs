using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] private float flySpeed = 10f;

    [SerializeField] private float rotationSpeed = 90f;

    public Vector2 CurrentVelocity => rb.linearVelocity;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        Vector3 playerScreenPosition =
            mainCamera.WorldToScreenPoint(rb.position);

        Vector3 mousePosition =
            Mouse.current.position.ReadValue();

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
}


