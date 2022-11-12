using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

// Note: structs are pass by copy not by reference like classes are!
public struct Speed : IComponentData
{
    public float value;
}
