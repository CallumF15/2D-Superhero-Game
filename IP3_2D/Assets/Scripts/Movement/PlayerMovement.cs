using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private float pitchDegrees;
    private float angle;

    public float flySpeed = 17.0f;
    public float rotationSpeed = 1.5f;
    public float distanceToSpawn = 10;

    public GameObject playerAttackObject;

    // Use this for initialization
    void Start()
    {

    }

    private void Update()
    {
        MousePlayerMovement();

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            playerAttack();
        }
    }

    void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Adjusts player rotation angle to mouse position
    /// </summary>
    public void CalculateMouseAngle()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }


    public void MousePlayerMovement()
    {
        CalculateMouseAngle();
        //transform.position += transform.right * Time.deltaTime * flySpeed; //move player
    }

    public void playerAttack()
    {
        //playerAttackObject.transform.localScale = new Vector3(-0.1f, 0.1f, 1);
        playerAttackObject.transform.localScale = new Vector3(-0.5f, 0.5f, 1);

        Vector3 playerPos = transform.position;
        Vector3 playerDirection = transform.right;
        Quaternion playerRotation = transform.rotation;

      

        Vector3 spawnPos = playerPos + playerDirection * distanceToSpawn;
        Debug.Log("spawnPos: " + spawnPos);
        GameObject obj = Instantiate(playerAttackObject, spawnPos, playerRotation);
        obj.AddComponent<playerAttack>();
    }
}
