using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct CubeComponantData : IComponentData
{
    public Entity PrefabEntity;
}
