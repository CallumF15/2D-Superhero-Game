using UnityEngine;
using System.Collections;

public class scaleAttack : MonoBehaviour
{
	[Range(1, 3)]
	public float lerpDuration = 1; //per second
	private float timeToDestroy = 5; //time until GameObject is removed
	private float valueToLerp, timeElapsed;

	private float minimumScale = 0.05f;
	private float maximumScale = 0.2f;

    private float facingDirection;

    private void Awake()
    {
        facingDirection = Mathf.Sign(transform.localScale.x);
    }

    /// <summary>
    ///  Scales the Objects size from minimum specified to max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    private void ScaleObject(float min, float max)
    {
        if (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(min, max, timeElapsed / lerpDuration);

            transform.localScale = new Vector3(valueToLerp * facingDirection, valueToLerp, 1f);

            timeElapsed += Time.deltaTime;
        }
        else
        {
            StartCoroutine(delayTimer(timeToDestroy));
            valueToLerp = max;
        }
    }

    // Update is called once per frame
    void Update ()
	{
		ScaleObject(minimumScale, maximumScale);
	}

	/// <summary>
	/// Adds a delay before method is called
	/// </summary>
	IEnumerator delayTimer(float delayTimer)
	{
		yield return new WaitForSeconds(delayTimer);
		DestroyAttackObject();
	}

	/// <summary>
	/// Destroys GameObject
	/// </summary>
	private void DestroyAttackObject()
	{
		Destroy(gameObject);
	}
}

