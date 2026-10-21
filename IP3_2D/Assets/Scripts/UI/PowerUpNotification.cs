using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpNotification : MonoBehaviour
{
    public static PowerUpNotification Instance { get; private set; }

    [SerializeField] private Text message;
    [SerializeField] private float disappearTime = 2.0f;

    private Coroutine clearMessageCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        message.text = "";
    }

    public void DisplayMessage(string powerupMessage)
    {
        message.text = powerupMessage;

        // Reset the timer if another message is displayed
        if (clearMessageCoroutine != null)
        {
            StopCoroutine(clearMessageCoroutine);
        }

        clearMessageCoroutine = StartCoroutine(DelayTimer());
    }

    private IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(disappearTime);

        message.text = "";
        clearMessageCoroutine = null;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}