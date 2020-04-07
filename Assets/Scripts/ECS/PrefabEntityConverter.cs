using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class PrefabEntityConverter : MonoBehaviour
{
    public static PrefabEntityConverter Instance;

    public GameObject GameObjectCubePrefab;
    public static Entity EnityCubePrefab;

    public GameObject GameObjectGroundPrefab;
    public static Entity EnityGroundPrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ConvertPrefabs();
        }
        else
        {
            Destroy(this);
        }
    }

    public void ConvertPrefabs()
    {
        using (BlobAssetStore blobAssetStore = new BlobAssetStore())
        {
            
            World world = World.DefaultGameObjectInjectionWorld;
            var settings = GameObjectConversionSettings.FromWorld(world, blobAssetStore);
            if (GameObjectCubePrefab != null)
            {
                EnityCubePrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(GameObjectCubePrefab, settings);
            }

            if (GameObjectGroundPrefab != null)
            {
                EnityGroundPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(GameObjectGroundPrefab,settings);
            }
        }
    }
}
