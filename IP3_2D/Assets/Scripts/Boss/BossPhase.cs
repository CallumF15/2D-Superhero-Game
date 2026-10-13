using System.Collections.Generic;
using UnityEngine;
using static BossAttack;

[System.Serializable]
public class BossPhase
{
    public string phaseName;

    [Range(0f, 1f)]
    public float healthThreshold;

    public float delayBetweenAttacks;

    //public List<IBossAttack> attacks;
}