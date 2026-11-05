using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnRule
{
    public string spawnName;
    public ObjectSpawner spawner;
    public float spawnInterval = 2f;

    [NonSerialized] public float timer;
}
