using UnityEngine;
using UnityEngine.UIElements;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotationXSpeed = 50f;
    [SerializeField] private float rotationYSpeed = 50f;
    [SerializeField] private float rotationZSpeed = 50f;

    public Transform childCollider;

    void Update()
    {
        transform.Rotate(rotationXSpeed * Time.deltaTime, rotationYSpeed * Time.deltaTime, rotationZSpeed * Time.deltaTime);

        // Keep the child collider's rotation fixed
        if (childCollider != null)
        {
            childCollider.rotation = Quaternion.identity;
        }
    }
}
