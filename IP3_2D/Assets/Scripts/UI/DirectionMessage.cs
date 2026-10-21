using UnityEngine.UI;
using UnityEngine;

public class DirectionMessage : MonoBehaviour
{
    [SerializeField] private Text message;
    [SerializeField] private GameObject player;
    [SerializeField] private float timeRemaining = 11.0f; //time left to return or player dies

    private float timeLeft;
    private float previousPositionX;

    void Start()
    {
        timeLeft = timeRemaining;
        previousPositionX = -16;
    }

    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        UpdateDirectionMessage(playerPosition);
    }

    private void UpdateDirectionMessage(Vector3 playerPosition)
    {
        // Player moving backwards
        if (playerPosition.x < previousPositionX)
        {
            DisplayDirectionMessage();
        }
        // Player moving forwards
        else if (playerPosition.x > previousPositionX)
        {
            timeRemaining = timeLeft;
            message.text = "";
        }

        previousPositionX = playerPosition.x;
    }


    /// <summary>
    /// Displays wrong direction message with the remaining time.
    /// </summary>
    private void DisplayDirectionMessage()
    {
        timeRemaining -= Time.deltaTime;
        int timeLeft = (int)timeRemaining;

        message.text = "Wrong direction! " + timeLeft.ToString() + " seconds to turn round"; ;

        if (timeRemaining < 0)
        {
            message.text = "GAME OVER!";
            Destroy(player);
        }
    }
}
