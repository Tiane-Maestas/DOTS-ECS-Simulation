using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public partial class ParticleSpawnerSystem : SystemBase
{
    public int spawnAmount = 10;
    protected override void OnUpdate()
    {
        // Query all particles by custom tag to get the current count.

        // Below is commented out for now so I can implement using traditional methods.
        // EntityQuery particleQuery = EntityManager.CreateEntityQuery(typeof(ParticleTag));

        // if (particleQuery.CalculateEntityCount() < spawnAmount)
        // {
        //     // Instantiate particle if less than spawn amount through a command buffer.
        //     ParticleSpawnerComponent particleSpawnerComponent = SystemAPI.GetSingleton<ParticleSpawnerComponent>();

        //     EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
        //     Entity particle = entityCommandBuffer.Instantiate(particleSpawnerComponent.particlePrefab);

        //     // Need to use reference here because stucts copy.
        //     RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

        //     entityCommandBuffer.SetComponent(particle, new Speed
        //     {
        //         // position = new Unity.Mathematics.float3(randomComponent.ValueRW.random.NextFloat(1f, 20f),
        //         //                                         randomComponent.ValueRW.random.NextFloat(1f, 20f),
        //         //                                         randomComponent.ValueRW.random.NextFloat(1f, 20f)),
        //         value = randomComponent.ValueRW.random.NextFloat(1f, 20f),
        //     });
        // }
    }
}
