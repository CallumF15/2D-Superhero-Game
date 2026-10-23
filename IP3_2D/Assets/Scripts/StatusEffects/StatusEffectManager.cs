using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    private IHealth health;
    private readonly List<StatusEffects> activeEffects = new();


    private void Awake()
    {
        health = GetComponent<IHealth>();
    }

    public void ApplyEffect(StatusEffects effect)
    {
        if (health == null || effect == null)
            return;

        activeEffects.Add(effect);
        effect.Apply(health);
    }

    public void RemoveEffect(StatusEffects effect)
    {
        if (effect == null || !activeEffects.Remove(effect))
            return;

        effect.Remove(health);
    }

    public void RemoveAllEffects()
    {
        foreach (StatusEffects effect in activeEffects)
        {
            effect.Remove(health);
        }

        activeEffects.Clear();
    }

    private void OnDisable()
    {
        RemoveAllEffects();
    }
}

