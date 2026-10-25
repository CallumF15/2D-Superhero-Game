using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger detected: " + other.name+ ", Tag: " + other.tag);

        Debug.Log("Hit object: " + other.name);
        Debug.Log("Parent: " + other.transform.parent);

        if (!other.CompareTag("Player"))
            return;

        
        ApplyEffect(other.gameObject);
        DisplayMessage();

        // Destroy the PowerUp's parent
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected abstract void DisplayMessage();

    protected abstract void ApplyEffect(GameObject player);
}