
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
//public class BossMovement : MonoBehaviour
//{
//    public float delayTime = 1.0f;
//    public float offsetX = 30;

//    [SerializeField] private Transform player;


//    [SerializeField] private float ySmoothTime = 0.25f;  // seconds to catch up; lower = tighter
//    [SerializeField] private float maxYSpeed = 30f;      // optional cap

//    private float yVelocity; // used internally by SmoothDamp

//    private void Awake()
//    {

//    }

//    private void Update()
//    {
//        if (player == null) return;

//        Vector3 pos = transform.position;

//        pos.x = player.position.x + offsetX;
//        pos.y = Mathf.SmoothDamp(pos.y, player.position.y, ref yVelocity, ySmoothTime, maxYSpeed);

//        transform.position = pos;
//    }
//}


public class BossMovement : MonoBehaviour, IMoveable
{
    public float delayTime = 1.0f;
    public float offsetX = 30;

    [SerializeField] private Transform player;

    [SerializeField] private float ySmoothTime = 0.25f;
    [SerializeField] private float maxYSpeed = 30f;

    private float yVelocity;
    public float VerticalSpeed => yVelocity;

    public Vector2 CurrentVelocity { get; private set; }

    private void Update()
    {
        if (player == null) return;

        Vector3 pos = transform.position;

        pos.x = player.position.x + offsetX;
        pos.y = Mathf.SmoothDamp(
            pos.y,
            player.position.y,
            ref yVelocity,
            ySmoothTime,
            maxYSpeed
        );

        transform.position = pos;
    }
}