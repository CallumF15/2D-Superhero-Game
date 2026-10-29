using System.Collections;
using UnityEngine;

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

                spawnPosition = new Vector3(spawnPosition.x - 1, spawnPosition.y, spawnPosition.z); //maybe make this easily assible via editor?

                bossGunTargeting.SetProjectileSpeed(settings.shotSpeed);

                Projectile projectile = Instantiate(prefab, spawnPosition,
                    Quaternion.Euler(
                        0f,
                        0f,
                        angle
                    ),
                     attackContainer
                );

                AttachScript(projectile, settings);


                projectile.Initialize(settings.damage, settings.shotSpeed, firePoint.right, firePoint.parent.gameObject);
            }

            yield return new WaitForSeconds(settings.shotDelay);
        }
    }


    //public override IEnumerator Execute(AttackSettings settings, Transform firePoint, Projectile prefab,Transform attackContainer)
    //{
    //    int volleyCount = settings.shotCount / 3;
    //    int shotPerVolley = 3;

    //    Vector3[] offsets =
    //    {
    //        new Vector3(0f, 5.0f, 0f),
    //        new Vector3(0f, 0f, 0f),
    //        new Vector3(0f, -5.0f, 0f)
    //    };

    //    for (int volley = 0; volley < volleyCount; volley++)
    //    {
    //        for (int i = 0; i < shotPerVolley; i++)
    //        {
    //            // Get angle relative to gun rotation
    //            ////float gunAngle = firePoint.eulerAngles.z;
    //            //float angleOffset = settings.angles[i % settings.angles.Length];

    //            //float finalAngle = gunAngle + angleOffset; //offset attack angle from gun angle

    //            // Spawn projectile relative to firepoint
    //            Vector3 spawnPosition = firePoint.position + Vector3.up * offsets[i].y;

    //            // Optional spawn position adjustment
    //            //spawnPosition += firePoint.right * -1f;

    //            float angleOffset = settings.angles[i % settings.angles.Length];

    //            Quaternion projectileRotation =
    //                firePoint.rotation * Quaternion.Euler(0f, 0f, angleOffset);

    //            Projectile projectile = Instantiate(
    //                prefab,
    //                spawnPosition,
    //                projectileRotation,
    //                attackContainer
    //            );

    //            AttachScript(projectile, settings);

    //            projectile.InitializeBoss(
    //                settings.damage,
    //                settings.shotSpeed
    //            );
    //        }

    //        yield return new WaitForSeconds(settings.shotDelay);
    //    }
    //}


}
