
using UnityEngine;
using System.Collections;

public class ScaleAttack : MonoBehaviour
{
    [Header("Scale Settings")]
    [SerializeField] private float minimumScale = 0.05f;
    [SerializeField] private float maximumScale = 0.2f;

    [Header("Animation Settings")]
    [SerializeField, Min(0.01f)] private float lerpDuration = 1f;

    [Header("Lifetime Settings")]
    [SerializeField, Min(0f)] private float timeToDestroy = 5f;

    private Vector3 originalScale;
    private float timeElapsed;
    private bool isScalingComplete;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (isScalingComplete)
            return;

        ScaleObject();
    }

    /// <summary>
    /// Smoothly scales the object from minimum to maximum size.
    /// </summary>
    private void ScaleObject()
    {
        timeElapsed += Time.deltaTime;

        float progress = Mathf.Clamp01(timeElapsed / lerpDuration);

        float currentScale = Mathf.Lerp(minimumScale, maximumScale, progress);

        transform.localScale = new Vector3(currentScale * Mathf.Sign(originalScale.x), currentScale * Mathf.Sign(originalScale.y), originalScale.z);

        if (progress >= 1f)
        {
            isScalingComplete = true;
            StartCoroutine(DestroyAfterDelay());
        }
    }

    /// <summary>
    /// Waits before destroying the object.
    /// </summary>
    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(timeToDestroy);

        Destroy(gameObject);
    }
}
