using UnityEngine;
using static BossAttack;

[System.Serializable]
public class AttackSettings
{
    public AttackElementType attackElementType;

    [Header("Attack Settings")]

    public float damage = 1f; //damage of the attack

    [Min(1)]
    public int shotCount = 1; //number of shots in the attack

    [Min(0f)]
    public float shotDelay = 1f; //delay between shots

    [Min(0f)]
    public float shotSpeed = 5f; //speed of the projectile

    public bool isTargeted; //whether the attack tracks the player

    [Header("Attack Pattern Direction")]
    public float[] angles = { 0f };  //the angles for the attack pattern, in degrees
}
