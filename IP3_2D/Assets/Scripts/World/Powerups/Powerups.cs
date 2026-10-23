using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger detected: " + other.name+ ", Tag: " + other.tag);

        if (!other.CompareTag("Player"))
            return;

        
        ApplyEffect(other.gameObject);
        DisplayMessage();

        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Debug.Log("This GameObject has no parent!");
        }
    }

    protected abstract void DisplayMessage();

    protected abstract void ApplyEffect(GameObject player);
}