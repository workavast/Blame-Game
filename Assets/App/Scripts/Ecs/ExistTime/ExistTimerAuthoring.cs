using Unity.Entities;
using UnityEngine;

namespace App.Ecs.ExistTime
{
    public class ExistTimerAuthoring : MonoBehaviour
    {
        [SerializeField] private float existTime;

        private class Baker : Baker<ExistTimerAuthoring>
        {
            public override void Bake(ExistTimerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new ExistTimer() { Value = authoring.existTime });
            }
        }
    }
}