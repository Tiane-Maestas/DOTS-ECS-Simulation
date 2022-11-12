using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class ParticleTagAuthoring : MonoBehaviour
{

}

public class ParticleTagBaker : Baker<ParticleTagAuthoring>
{
    public override void Bake(ParticleTagAuthoring authoring)
    {
        AddComponent(new ParticleTag());
    }
}
