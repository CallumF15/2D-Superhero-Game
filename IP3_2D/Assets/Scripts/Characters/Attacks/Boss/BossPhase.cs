using System;
using UnityEngine;

[Serializable]
public class BossPhase
{
    public string phaseName;
    [Range(0f, 100f)]
    public float healthThreshold;

    public float attackIntervalMultiplier = 1f;

    public float attackDelayMultplier = 1f;
    public float damageMultiplier = 1f;
    public float projectileSpeedMultiplier = 1f;
    public int extraShots = 0;
}

