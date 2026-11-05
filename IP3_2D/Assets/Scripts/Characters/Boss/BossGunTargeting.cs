using UnityEngine;


using UnityEngine;

public class BossGunTargeting : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float shotSpeed = 50f;       // match your attack's shotSpeed
    [SerializeField, Range(0f, 1f)] private float leadAmount = 0.8f;
    [SerializeField] private float rotateSpeed = 360f;    // degrees/sec, 0 = snap instantly

    private void Update()
    {
        if (playerRb == null) return;

        Vector2 gunPos = transform.position;
        Vector2 direct = ((Vector2)playerRb.position - gunPos).normalized;

        TryGetInterceptDirection(gunPos, playerRb.position, playerRb.linearVelocity, shotSpeed, out Vector2 lead);

        Vector2 dir = Vector2.Lerp(direct, lead, leadAmount).normalized;

        // your shots travel along -right, so -right must point along dir
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;

        float angle = rotateSpeed > 0f
            ? Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.deltaTime)
            : targetAngle;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public static bool TryGetInterceptDirection(Vector2 shooterPos, Vector2 targetPos, Vector2 targetVel, float projectileSpeed, out Vector2 dir)
    {
        Vector2 d = targetPos - shooterPos;

        float a = Vector2.Dot(targetVel, targetVel) - projectileSpeed * projectileSpeed;
        float b = 2f * Vector2.Dot(d, targetVel);
        float c = Vector2.Dot(d, d);

        float t;

        if (Mathf.Abs(a) < 0.0001f)
        {
            // shot speed == target speed: equation becomes linear
            if (Mathf.Abs(b) < 0.0001f) { dir = d.normalized; return false; }
            t = -c / b;
        }
        else
        {
            float disc = b * b - 4f * a * c;
            if (disc < 0f) { dir = d.normalized; return false; } // can't catch him

            float sqrt = Mathf.Sqrt(disc);
            float t1 = (-b - sqrt) / (2f * a);
            float t2 = (-b + sqrt) / (2f * a);

            // smallest positive time
            t = (t1 > 0f && t2 > 0f) ? Mathf.Min(t1, t2) : Mathf.Max(t1, t2);
        }

        if (t <= 0f) { dir = d.normalized; return false; }

        dir = (d + targetVel * t).normalized; // aim at where he will be
        return true;
    }
}
