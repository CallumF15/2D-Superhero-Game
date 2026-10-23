using UnityEngine;
using System.Collections;

public class BurnEffect : StatusEffects
{
    private readonly MonoBehaviour runner;
    private readonly float damage;
    private readonly float interval;
    private readonly float duration;

    private IHealth target;
    private Coroutine burnCoroutine;

    public BurnEffect(MonoBehaviour runner, float damage, float interval, float duration)
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
        burnCoroutine = runner.StartCoroutine(BurnRoutine());
    }

    private IEnumerator BurnRoutine()
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

        burnCoroutine = null;
        target = null;
    }

    public override void Remove(IHealth target)
    {
        if (burnCoroutine != null)
        {
            runner.StopCoroutine(burnCoroutine);
            burnCoroutine = null;
        }

        this.target = null;
    }
}
