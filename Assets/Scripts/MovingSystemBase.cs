using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

// Class Managed. Simple, on main thread, cannot use burst, less optimal.
public partial class MovingSystemBase : SystemBase
{
    protected override void OnUpdate()
    {
        // RefRO<Speed> -> Reference Read Only
        foreach ((TransformAspect transformAspect, RefRO<Speed> speed) in SystemAPI.Query<TransformAspect, RefRO<Speed>>())
        {
            // transformAspect.Position += new float3(speed.ValueRO.value * SystemAPI.Time.DeltaTime, 0, 0);
            // transformAspect.TranslateWorld();
        }
    }
}
