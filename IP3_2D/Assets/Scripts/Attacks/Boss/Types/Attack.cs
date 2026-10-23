using System.Collections;
using UnityEditor;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected Transform projectilePoint;
    protected AttackSettings settings;

    public abstract IEnumerator Execute(AttackSettings settings, Transform firePoint, Projectile prefab, Transform attackContainer);


    public void AttachScript(Projectile projectile, AttackSettings settings)
    {
        BossAttackMovement attackMove = projectile.GetComponent<BossAttackMovement>();
        TargetedAttack targetedAttack = projectile.GetComponent<TargetedAttack>();

        if (attackMove == null || targetedAttack == null)
        {
            Debug.LogError("Missing AttackMove or TargetedAttack on projectile!");
            return;
        }

        if (settings.isTargeted)
        {
            attackMove.enabled = true;
            targetedAttack.enabled = false;

            Debug.Log($"{projectile.name}: Basic movement started at {Time.time}");

            StartCoroutine(
                EnableTrackingAfterDelay(projectile, targetedAttack, attackMove, 1f)
            );
        }
        else
        {
            attackMove.enabled = true;
            targetedAttack.enabled = false;
        }
    }

    private IEnumerator EnableTrackingAfterDelay(Projectile projectile, TargetedAttack attackTarget, BossAttackMovement attackMove, float delay)
    {
        Debug.Log($"Waiting {delay} seconds before targeting.");
        yield return new WaitForSeconds(delay);

        if (projectile == null)
            yield break;

        Debug.Log($"{projectile.name}: Switching to targeting at {Time.time}");

        attackMove.enabled = false;
        attackTarget.enabled = true;
    }
}