using System.Collections;
using UnityEngine;

public class MultipleFireAttack : Attack
{
    public override IEnumerator Execute(AttackSettings settings, Transform firePoint, Projectile prefab, Transform attackContainer)
    {
        for (int i = 0; i < settings.shotCount; i++)
        {
            Projectile projectile = Instantiate(
                prefab,
                firePoint.position,
                firePoint.rotation,
                attackContainer
            );

            AttachScript(projectile, settings);

            projectile.Initialize(settings.damage, settings.shotSpeed, firePoint.right, firePoint.parent.gameObject);

            yield return new WaitForSeconds(settings.shotDelay);
        }
    }
}
