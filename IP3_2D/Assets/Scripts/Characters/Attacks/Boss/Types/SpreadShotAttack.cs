using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SpreadShotAttack : Attack
{
    public BossGunTargeting bossGunTargeting;
    //old code may reuse
    public override IEnumerator Execute(AttackSettings settings, Transform firePoint, Projectile prefab, Transform attackContainer)
    {
        int volleyCount = settings.shotCount / 3;
        int shotPerVolley = 3;


        Vector3[] offsets =
        {
            new Vector3(0f, 5.0f, 0f),
            new Vector3(0f, 0f, 0f),
            new Vector3(0f, -5.0f, 0f)
        };

        for (int volley = 0; volley < volleyCount; volley++)
        {
            for (int i = 0; i < shotPerVolley; i++)
            {
                float angle = settings.angles[i % settings.angles.Length];
                Vector3 spawnPosition = firePoint.TransformPoint(offsets[i]);

                Vector2 projectileDirection = Quaternion.Euler(0f, 0f, angle) * (Vector2)firePoint.right;

                spawnPosition = new Vector3(spawnPosition.x - 1, spawnPosition.y, spawnPosition.z); //maybe make this easily assible via editor?

                //bossGunTargeting.SetProjectileSpeed(settings.shotSpeed);

                Projectile projectile = Instantiate(prefab, spawnPosition,
                    Quaternion.Euler(
                        0f,
                        0f,
                        angle
                    ),
                     attackContainer
                );

                AttachScript(projectile, settings);

                projectile.Initialize(settings.damage, settings.shotSpeed, projectileDirection, firePoint.parent.gameObject);
            }

            yield return new WaitForSeconds(settings.shotDelay);
        }
    }
}
