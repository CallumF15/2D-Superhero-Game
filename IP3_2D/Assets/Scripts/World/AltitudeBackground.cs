using UnityEngine;


using UnityEngine;

public class AltitudeBackground : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Camera targetCamera;

    [Header("Altitude thresholds")]
    [SerializeField] private float skyAltitude = 0f;
    [SerializeField] private float darkBlueAltitude = 500f;
    [SerializeField] private float navyAltitude = 1500f;
    [SerializeField] private float blackAltitude = 2500f;

    [Header("Background colours")]
    [SerializeField]private Color skyBlue = new Color(0.35f, 0.73f, 0.95f);

    [SerializeField] private Color darkBlue = new Color(0.10f, 0.28f, 0.52f);

    [SerializeField] private Color navy = new Color(0.025f, 0.035f, 0.09f);

    [SerializeField] private Color spaceBlack = Color.black;

    private void Awake()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (player == null || targetCamera == null)
            return;

        float altitude = player.position.y;

        if (altitude < darkBlueAltitude)
        {
            float t = Mathf.InverseLerp(skyAltitude, darkBlueAltitude, altitude);
            targetCamera.backgroundColor = Color.Lerp(skyBlue, darkBlue, t);
        }
        else if (altitude < navyAltitude)
        {
            float t = Mathf.InverseLerp(darkBlueAltitude, navyAltitude, altitude);
            targetCamera.backgroundColor = Color.Lerp(darkBlue, navy, t);
        }
        else
        {
            float t = Mathf.InverseLerp(navyAltitude, blackAltitude, altitude);
            targetCamera.backgroundColor = Color.Lerp(navy, spaceBlack, t);
        }
    }
}
