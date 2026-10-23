using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpNotification : MonoBehaviour
{
    public static PowerUpNotification Instance { get; private set; }

    [SerializeField] private Text message;
    [SerializeField] private float disappearTime = 2f;
    [SerializeField] private float messageDelay = 0.3f;

    private readonly Queue<string> messageQueue = new Queue<string>();
    private Coroutine messageCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Keep the UI object active; start with no visible text.
        message.text = "";
    }

    public void DisplayMessage(string powerupMessage)
    {
        messageQueue.Enqueue(powerupMessage);

        // Start processing only if a queue coroutine isn't already running.
        if (messageCoroutine == null)
        {
            messageCoroutine = StartCoroutine(ProcessMessageQueue());
        }
    }

    private IEnumerator ProcessMessageQueue()
    {
        while (messageQueue.Count > 0)
        {
            // Display the oldest queued message.
            message.text = messageQueue.Dequeue();

            yield return new WaitForSeconds(disappearTime);

            // Clear the message before displaying the next one.
            message.text = "";

            // Add a short delay only when another message is waiting.
            if (messageQueue.Count > 0)
            {
                yield return new WaitForSeconds(messageDelay);
            }
        }

        // Allow a new coroutine to start when another message arrives.
        messageCoroutine = null;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}