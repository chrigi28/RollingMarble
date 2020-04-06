using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class LevelGenerationSystem : ComponentSystem
{
    private Random random;
    private bool generated;

    protected override void OnCreate()
    {
        random = new Random();
    }

    protected override void OnUpdate()
    {
        if (!this.generated)
        {
            Entity cubeEntity = EntityManager.Instantiate(PrefabEntities.PrefabEntity);
            EntityManager.SetComponentData(cubeEntity,
                new Translation
                    {Value = new float3(random.NextFloat(-5, 5), random.NextFloat(2), random.NextFloat(-5, 5))});
            EntityManager.SetComponentData(cubeEntity, new Rotation {Value = random.NextQuaternionRotation()});
            this.generated = true;
        }
    }
}
