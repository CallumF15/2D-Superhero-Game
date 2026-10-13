using UnityEngine;
using System.Collections;

public class scaleAttack : MonoBehaviour
{
	[Range(1, 3)]
	public float lerpDuration = 3; //per second
	private float timeToDestroy = 5; //time until GameObject is removed
	private float valueToLerp, timeElapsed;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		ScaleObject(transform.localScale.x, .5f);
	}

	/// <summary>
	/// Scales the Objects size
	/// </summary>
	private void ScaleObject(float min, float max)
    {
		if (timeElapsed < lerpDuration)
		{
			valueToLerp = Mathf.Lerp(min, max, timeElapsed / lerpDuration);
			transform.localScale = new Vector3(valueToLerp, valueToLerp, 1);
			timeElapsed += Time.deltaTime;
		}
		else
		{
			StartCoroutine(delayTimer(timeToDestroy));
			valueToLerp = max;
		}
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

