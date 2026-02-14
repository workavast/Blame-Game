using System;
using App.ResourcesSystem;
using AYellowpaper.SerializedCollections;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.ResourcesDropping
{
    public class ResourcesDropperAuthoring : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<ResourceType, MinMaxAmount> resources;
        
        private class Baker : Baker<ResourcesDropperAuthoring>
        {
            public override void Bake(ResourcesDropperAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                foreach (var resource in authoring.resources) 
                    AddResourceDropComponent(entity, resource.Key, resource.Value);
            }
            
            private void AddResourceDropComponent(Entity entity, ResourceType resource, MinMaxAmount amount)
            {
                switch (resource)
                {
                    case ResourceType.Scrap:
                        AddComponent(entity, new ScrapDropper
                        {
                            MinAmount = amount.MinAmount,
                            MaxAmount = amount.MaxAmount
                        });
                        break;
                    case ResourceType.Plasma:
                        AddComponent(entity, new PlasmaDropper()
                        {
                            MinAmount = amount.MinAmount,
                            MaxAmount = amount.MaxAmount
                        });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        [Serializable]
        private struct MinMaxAmount
        {
            [SerializeField] private int minAmount;
            [SerializeField] private int maxAmount;
            
            public int MinAmount => minAmount;
            public int MaxAmount => maxAmount;
        }
    }
}