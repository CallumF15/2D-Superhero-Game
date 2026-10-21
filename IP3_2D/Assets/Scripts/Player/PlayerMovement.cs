using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private float angle;

    // Stores a reference to the main camera.
    // We use this to convert the player's world position
    // into screen coordinates.
    private Camera mainCamera;

    [SerializeField] private float flySpeed = 17.0f;
    [SerializeField] private float distanceToSpawn = 10.0f;
    [SerializeField] private GameObject playerAttackObject;
    [SerializeField] private float rotationSpeed = 90f;

    public float FlySpeed { get => flySpeed;}

    //[SerializeField] = "These are settings I want to configure in Unity, but other scripts shouldn't directly modify them.

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Calculate the angle between the player
        // and the mouse and rotate the player toward it.
        CalculateMouseAngle();
        transform.position += transform.right * Time.deltaTime * flySpeed; //move player

        

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAttack();
        }

    }

    private void CalculateMouseAngle()
    {
        Vector3 playerScreenPosition = mainCamera.WorldToScreenPoint(transform.position);

        Vector3 direction = Input.mousePosition - playerScreenPosition;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    private void PlayerAttack()
    {
        // Get the player's current position in the world.
        Vector3 playerPosition = transform.position;

        // Get the direction the player is currently facing.
        // transform.right is the player's local X axis.
        // This means your player is expected to face right
        // when its rotation is zero.
        Vector3 playerDirection = transform.right;

        // Get the player's current rotation.
        Vector3 playerRotation = transform.rotation.eulerAngles;

        // Calculate where the attack should appear
        // Start at the player and move forward by
        // distanceToSpawn units.
        Vector3 spawnPosition = playerPosition + playerDirection * distanceToSpawn;

        // Print the spawn position to the Console.
        //Debug.Log("spawnPos: " + spawnPosition);

        // Create a new copy of the attack prefab at the
        // calculated position and rotation.
        playerAttackObject.GetComponent<PlayerAttackMovement>().speed = flySpeed + 10; //set speed of attack to match player speed
        GameObject obj = Instantiate(playerAttackObject, spawnPosition, transform.rotation);

        Debug.Log("Projectile spawn position: " + obj.transform.position);
        Debug.Log("Projectile rotation: " + obj.transform.eulerAngles);

        // Set the scale of the newly created attack object.
        // This changes the spawned object rather than the
        // original prefab/reference.
        obj.transform.localScale = new Vector3(-.5f, 0.5f, 1.0f);
    }
}
