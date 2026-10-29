public abstract class StatusEffects
{
    public float Duration { get; protected set; }

    protected StatusEffects(float duration)
    {
        this.Duration = duration;
    }

    public abstract void Apply(Health target);
    public abstract void Remove(Health target);
}