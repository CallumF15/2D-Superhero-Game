using UnityEngine;


/// <summary>
/// Keeps the Boss Health UI above the boss
/// </summary>
public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Transform healthCanvas;
    [SerializeField] private Vector3 offset = new Vector3(0f, 4f, 0f);

    private void LateUpdate()
    {
        if (healthCanvas == null)
            return;

        healthCanvas.position = transform.position + offset;
    }
}