using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;

[BurstCompile]
public partial struct MovingISystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // RefRO<Speed> -> Reference Read Only
        // foreach ((TransformAspect transformAspect, RefRO<Speed> speed) in SystemAPI.Query<TransformAspect, RefRO<Speed>>())
        // {
        //     transformAspect.Position += new Unity.Mathematics.float3(speed.ValueRO.value * SystemAPI.Time.DeltaTime, 0, 0);
        //     // transformAspect.TranslateWorld();
        // }

        // Runs the same logic as system base but on seperate worker threads in parallel.
        float deltaTime = SystemAPI.Time.DeltaTime;
        new SomeJob
        {
            deltaTime = deltaTime,
        }.ScheduleParallel();
    }
}

[BurstCompile]
public partial struct SomeJob : IJobEntity
{
    public float deltaTime;
    public void Execute(TransformAspect transformAspect, Speed speed)
    {
        transformAspect.Position += new Unity.Mathematics.float3(speed.value * deltaTime, 0, 0);
    }
}