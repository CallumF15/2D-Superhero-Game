using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHeightRestrictor : MonoBehaviour {

	public GameObject player;
	public Text message;

	public static float playerHeightLimit = 100.0f; //maximum height limit
    [SerializeField] private float timeRemaining = 11.0f; //time left to return or player dies



    private float timeLeft;

	private float currentPosX;
	private float prevPosX;

	// Use this for initialization 
	void Start () {
		timeLeft = timeRemaining;
		prevPosX = -16;
		currentPosX = 1;
	}
	
	// Update is called once per frame
	void Update () {
        LimitHeight();
	}


    private void LimitHeight()
    {
        float currentPosX = player.transform.position.x;
        float currentPosY = player.transform.position.y;

        // Clamp player's height
        currentPosY = Mathf.Clamp(currentPosY, 0f, playerHeightLimit);

        Vector3 playerPos = player.transform.position;
        playerPos.y = currentPosY;
        player.transform.position = playerPos;

        // Player moving backwards
        if (currentPosX < prevPosX)
        {
            WrongDirectionMessage();
        }
        // Player moving forwards
        else if (currentPosX > prevPosX)
        {
            timeRemaining = timeLeft;
            message.text = "";
        }

        prevPosX = currentPosX;
    }


	/// <summary>
	/// Displays message with time remaining 
	/// </summary>
	private void WrongDirectionMessage()
	{
		timeRemaining -= Time.deltaTime;
		int timeLeft = (int)timeRemaining;

		message.text = "Wrong direction! " + timeLeft.ToString() + " seconds to turn round"; ;

		if (timeRemaining < 0) {
			message.text = "GAME OVER!";
			Destroy(player);
		}
	}
}
