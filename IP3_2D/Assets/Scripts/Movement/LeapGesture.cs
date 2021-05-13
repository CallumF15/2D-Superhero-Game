using UnityEngine;
using System.Collections;

public class LeapGesture : MonoBehaviour
{

    public float degreeX { get; set; }
    public float degreeY { get; set; }
    public float degreeZ { get; set; }
    public float pitchDegrees { get; set; }
    public float yawDegrees { get; set; }
    public float rollDegrees { get; set; }

    private Movement movement;
    private float fixedAngle = 90.0f;

    // Use this for initialization
    void Start()
    {
        movement = gameObject.AddComponent<Movement>();
    }

    void FixedUpdate()
    {
        HandPlayerMovement();
    }

    /// <summary>
    /// Should adjust player angle to mouse position
    /// </summary>
    public void CalculateMouseAngle()
    {
        //pitchDegrees = gameObject.transform.eulerAngles.z;

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }


    public void HandPlayerMovement()
    {
        MovementType type;
        CalculateMouseAngle();

        //if (pitchDegrees >= -20 && pitchDegrees <= 20)
        //{
        //	type = MovementType.stand;
        //	movement.CheckMovementType(type);


        //	//note: if we have up/down movement with pitch it'll collide with left/right gestures
        //	//note: since pitch goes to negative it makes our speed negative :/
        //	//note: since our Flying code sets pitch to 45, this code will not work correctly unless we resolve this someway

        //	//			if(pitchDegrees <= 45 && pitchDegrees <= -45){
        //	//				type = MovementType.flyAscend;
        //	//				movement.CheckMovementType (type, movement.getfaceRight);
        //	//			}
        //}

        //print("PITCH DEGREEES: " + pitchDegrees);


        //Flying code: 
        //note:
        if (pitchDegrees <= fixedAngle && pitchDegrees >= -fixedAngle)
        {
            type = MovementType.fly;
            movement.getPitchAngle = pitchDegrees;
            movement.CheckMovementType(type);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            type = MovementType.fire;
            movement.CheckMovementType(type);
        }

    }
}
