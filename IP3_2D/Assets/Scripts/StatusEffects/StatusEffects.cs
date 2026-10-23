public abstract class StatusEffects
{
    public float Duration { get; protected set; }

    protected StatusEffects(float duration)
    {
        this.Duration = duration;
    }

    public abstract void Apply(IHealth target);
    public abstract void Remove(IHealth target);
}