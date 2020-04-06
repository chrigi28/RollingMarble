using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[UpdateBefore(typeof(LevelGenerationSystem))]
public class PrefabEntities : MonoBehaviour, IConvertGameObjectToEntity
{
    public static Entity PrefabEntity;
    public GameObject PrefabGameObject;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        using (BlobAssetStore blobAssetStore = new BlobAssetStore())
        {
            Entity prefabEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(PrefabGameObject,
                GameObjectConversionSettings.FromWorld(dstManager.World, blobAssetStore));
            PrefabEntity = prefabEntity; 
        }
    }
}
