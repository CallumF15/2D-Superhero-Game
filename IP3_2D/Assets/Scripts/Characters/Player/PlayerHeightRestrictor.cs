using UnityEngine;

public class PlayerHeightRestrictor : MonoBehaviour 
{
	[SerializeField] private float maximumHeightValue = 1500.0f;
	[SerializeField] private float minimumHeightValue = 0f;

	// Update is called once per frame
	void Update () 
    {
		RestrictPlayerHeight(gameObject.transform.position);
	}

	private void RestrictPlayerHeight(Vector3 playerPosition)
	{
		// Prevent the player exceeding the specified height
		playerPosition.y = Mathf.Clamp(
			playerPosition.y,
			minimumHeightValue,
			maximumHeightValue
		);

        gameObject.transform.position = playerPosition;
	}
}
