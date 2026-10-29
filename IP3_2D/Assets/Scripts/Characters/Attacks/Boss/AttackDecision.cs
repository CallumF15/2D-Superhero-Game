using System.Collections;
using UnityEngine;

public class AttackDecision
{
    public Attack attack { get; }
    public AttackType Type { get; }
    public AttackElementType Element { get; }
    public AttackSettings Settings { get; set; }
    public Projectile Prefab { get; }

    public AttackDecision(Attack attack, AttackType type, AttackElementType element, AttackSettings settings, Projectile prefab)
    {
        this.attack = attack;
        Type = type;
        Element = element;
        Settings = settings;
        Prefab = prefab;
    }
}
