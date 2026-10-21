using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected: " + other.name+ ", Tag: " + other.tag);

        if (!other.CompareTag("Player"))
            return;

        
        ApplyEffect(other.gameObject);
        DisplayMessage();

        Destroy(gameObject);
    }

    protected abstract void DisplayMessage();

    protected abstract void ApplyEffect(GameObject player);
}