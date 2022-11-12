using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class ParticleSpawnerAuthoring : MonoBehaviour
{
    public GameObject particlePrefab;
}

public class ParticleSpawnerBaker : Baker<ParticleSpawnerAuthoring>
{
    public override void Bake(ParticleSpawnerAuthoring authoring)
    {
        AddComponent(new ParticleSpawnerComponent
        {
            particlePrefab = GetEntity(authoring.particlePrefab),
        });
    }
}
