using System.Collections;
using UnityEngine;

public class IceEffect : StatusEffects
{
    private readonly MonoBehaviour runner;
    private readonly float slowPercentage;
    private readonly float duration;

    private IHealth target;
    private Coroutine iceCoroutine;

    public IceEffect(MonoBehaviour runner, float slowPercentage, float duration)
        : base(duration)
    {
        this.runner = runner;
        this.slowPercentage = slowPercentage;
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
        // Apply slow
        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.DecreaseFlySpeed(player.FlySpeed * slowPercentage);

        yield return new WaitForSeconds(duration);

        // Restore movement speed
        player.RestoreOriginalFlySpeed();

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

        // TODO: Restore movement speed if effect is removed early

        this.target = null;
    }
}