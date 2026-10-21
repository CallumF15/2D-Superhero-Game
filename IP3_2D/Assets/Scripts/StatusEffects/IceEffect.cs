using System;
using System.Collections;
using UnityEngine;

public class IceEffect : StatusEffects
{
    private readonly MonoBehaviour runner;
    private readonly float damage;
    private readonly float interval;
    private readonly float duration;

    private IHealth target;
    private Coroutine iceCoroutine;

    public IceEffect(MonoBehaviour runner, float damage, float interval, float duration)
        : base(duration)
    {
        this.runner = runner;
        this.damage = damage;
        this.interval = interval;
        this.duration = duration;
    }

    public override void Apply(IHealth target)
    {
        Remove(this.target);

        this.target = target;
        iceCoroutine = runner.StartCoroutine(IceRoutine());
    }

    private IEnumerator IceRoutine()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            yield return new WaitForSeconds(interval);

            if (target == null)
                yield break;

            target.TakeDamage(damage);
            elapsed += interval;
        }

        iceCoroutine = null;
        target = null;
    }

    public override void Remove(IHealth target)
    {
        if (iceCoroutine != null)
        {
            runner.StopCoroutine(iceCoroutine);
            iceCoroutine = null;
        }

        this.target = null;
    }
}
