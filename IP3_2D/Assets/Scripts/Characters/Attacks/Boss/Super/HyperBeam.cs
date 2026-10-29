using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HyperBeam : MonoBehaviour
{
    [Header("Beam References")]
    [SerializeField] private LineRenderer outerBeam;
    [SerializeField] private LineRenderer innerBeam;

    [Header("Beam Settings")]
    [SerializeField] private float beamLength = 15f;
    [SerializeField] private float beamDuration = 1.5f;
    [SerializeField] private float growSpeed = 0.15f;

    [SerializeField] private float pulseSpeed = 10f;
    [SerializeField] private float pulseAmount = 0.15f;

    private Coroutine beamCoroutine;

    private void Awake()
    {
        SetupBeam(outerBeam);
        SetupBeam(innerBeam);

        outerBeam.enabled = false;
        innerBeam.enabled = false;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            Fire();
        }

        if (!outerBeam.enabled) return;

        float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

        outerBeam.widthMultiplier = pulse;
        innerBeam.widthMultiplier = pulse;
    }

    private IEnumerator FireBeam()
    {
        outerBeam.enabled = true;
        innerBeam.enabled = true;

        float elapsed = 0f;

        while (elapsed < growSpeed)
        {
            elapsed += Time.deltaTime;

            float currentLength = Mathf.Lerp(0f, beamLength, elapsed / growSpeed);

            UpdateBeam(currentLength);

            yield return null;
        }

        UpdateBeam(beamLength);

        yield return new WaitForSeconds(beamDuration);

        outerBeam.enabled = false;
        innerBeam.enabled = false;

        beamCoroutine = null;
    }

    private void SetupBeam(LineRenderer beam)
    {
        beam.useWorldSpace = false;
        beam.positionCount = 2;

        beam.SetPosition(0, Vector3.zero);
        beam.SetPosition(1, Vector3.zero);
    }

    public void Fire()
    {
        if (beamCoroutine != null)
        {
            StopCoroutine(beamCoroutine);
            beamCoroutine = null;
        }


        // Reset beam length
        UpdateBeam(0f);

        outerBeam.widthMultiplier = 1f;
        innerBeam.widthMultiplier = 1f;

        // Enable renderers
        outerBeam.enabled = true;
        innerBeam.enabled = true;

        beamCoroutine = StartCoroutine(FireBeam());
    }

    private void UpdateBeam(float length)
    {
        Vector3 endPosition = new Vector3(length, 0f, 0f);

        outerBeam.startWidth = 10f;
        outerBeam.endWidth = 10f;

        innerBeam.startWidth = 5f;
        innerBeam.endWidth = 5f;

        outerBeam.SetPosition(1, endPosition);
        innerBeam.SetPosition(1, endPosition);
    }
}
