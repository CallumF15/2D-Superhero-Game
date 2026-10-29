using UnityEngine;

public interface IProjectileMovement
{
    void Initialize(float speed, Vector2 direction, GameObject owner);
}