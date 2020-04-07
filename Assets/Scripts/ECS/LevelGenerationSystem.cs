using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;


public class LevelGenerationSystem : MonoBehaviour
{
    private Random random;
    private bool generated;

    void Awake()
    {
        random = new Random(3);
    }

    void Start()
    {
        EntityManager em = World.DefaultGameObjectInjectionWorld.EntityManager;

        Entity ground = em.Instantiate(PrefabEntityConverter.EnityGroundPrefab);
        Entity cubeEntity = em.Instantiate(PrefabEntityConverter.EnityCubePrefab);
        em.SetComponentData(cubeEntity, new Translation {Value = new float3(0,0,0)});
        ////em.SetComponentData(cubeEntity, new Translation {Value = new float3(random.NextFloat(-5, 5), random.NextFloat(2), random.NextFloat(-5, 5))});
        em.SetComponentData(cubeEntity, new Rotation {Value = random.NextQuaternionRotation()});
    }

    ////protected override void OnCreate()
    ////{
    ////    random = new Random();
    ////}

    ////protected override void OnUpdate()
    ////{
    ////    if (!this.generated)
    ////    {
    ////        Entity cubeEntity = EntityManager.Instantiate(PrefabEntities.PrefabEntity);
    ////        EntityManager.SetComponentData(cubeEntity,
    ////            new Translation
    ////                {Value = new float3(random.NextFloat(-5, 5), random.NextFloat(2), random.NextFloat(-5, 5))});
    ////        EntityManager.SetComponentData(cubeEntity, new Rotation {Value = random.NextQuaternionRotation()});
    ////        this.Enabled = false;
    ////    }
    ////}
}
