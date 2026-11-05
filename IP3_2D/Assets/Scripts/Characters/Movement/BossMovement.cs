
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class BossMovement : MonoBehaviour, IMoveable
{
    [SerializeField] private float offsetX = 30f;
    [SerializeField] private Rigidbody2D playerRb;   // drag the player here
    [SerializeField] private float ySmoothTime = 0.25f;
    [SerializeField] private float maxYSpeed = 30f;

    [SerializeField] private float flySpeed = 20f;

    private Rigidbody2D rb;
    private float yVelocity;

    public float VerticalSpeed => yVelocity;
    //public Vector2 CurrentVelocity { get; private set; }

    public Vector2 CurrentVelocity => rb.linearVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void FixedUpdate()
    {
        if (playerRb == null) return;

        Vector2 pos = rb.position;

        float x = playerRb.position.x + offsetX;
        float y = Mathf.SmoothDamp(pos.y, playerRb.position.y, ref yVelocity, ySmoothTime, flySpeed, Time.fixedDeltaTime);

        rb.MovePosition(new Vector2(x, y));
        //CurrentVelocity = new Vector2(0f, yVelocity);

        rb.linearVelocity = -(Vector2)transform.right * flySpeed;

    }
}

//public class BossMovement : MonoBehaviour, IMoveable
//{
//    public float delayTime = 1.0f;
//    public float offsetX = 30;

//    [SerializeField] private Transform player;

//    [SerializeField] private float ySmoothTime = 0.25f;
//    [SerializeField] private float maxYSpeed = 30f;

//    private float yVelocity;
//    public float VerticalSpeed => yVelocity;

//    public Vector2 CurrentVelocity { get; private set; }

//    private float lastY;

//    private void Start() => lastY = transform.position.y;

//    private void LateUpdate()
//    {
//        float y = transform.position.y;
//        CurrentVelocity = new Vector2(0f, (y - lastY) / Time.deltaTime);
//        lastY = y;
//    }

//    private void Update()
//    {
//        if (player == null) return;

//        Vector3 pos = transform.position;

//        pos.x = player.position.x + offsetX;
//        pos.y = Mathf.SmoothDamp(
//            pos.y,
//            player.position.y,
//            ref yVelocity,
//            ySmoothTime,
//            maxYSpeed
//        );

//        transform.position = pos;
//    }
//}