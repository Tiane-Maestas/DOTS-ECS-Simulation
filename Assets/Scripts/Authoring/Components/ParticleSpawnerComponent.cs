using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct ParticleSpawnerComponent : IComponentData
{
    public Entity particlePrefab;
}
