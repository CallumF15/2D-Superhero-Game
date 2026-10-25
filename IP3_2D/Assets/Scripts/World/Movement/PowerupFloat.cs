using UnityEngine;

public class PowerUpFloat : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float floatHeight = 0.25f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y +
                     Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(
            startPosition.x,
            newY,
            startPosition.z
        );
    }
}
