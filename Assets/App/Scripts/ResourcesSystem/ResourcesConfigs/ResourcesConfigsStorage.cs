using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace App.ResourcesSystem.ResourcesConfigs
{
    [CreateAssetMenu(fileName = nameof(ResourcesConfigsStorage), menuName = ResourcesConsts.Path + nameof(ResourcesConfigsStorage))]
    public class ResourcesConfigsStorage : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<ResourceType, ResourceConfig> configs = new();

        public ResourceConfig GetConfig(ResourceType resourceType)
        {
            if (configs.TryGetValue(resourceType, out var config))
                return config;

            Debug.LogError($"Config for resource type {resourceType} not found");
            return null;
        }

        public IReadOnlyDictionary<ResourceType, ResourceConfig> GetAllConfigs() => configs;
    }
}

