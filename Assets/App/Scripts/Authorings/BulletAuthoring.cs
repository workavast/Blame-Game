using App.Ecs;
using App.Views;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Authorings
{
    public class BulletAuthoring : MonoBehaviour
    {
        [SerializeField] private float existTime;
        [SerializeField] private WeakObjectReference<BulletView> viewPrefab;
        
        private class Baker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new IsActiveTag());
                
                AddComponent(entity, new BulletTag());
                AddComponent(entity, new ExistTimer() {Value = authoring.existTime});
                AddComponent(entity, new BulletPrefab() { Prefab = authoring.viewPrefab });
                AddComponent(entity, new AttackDamage());
                
                AddComponent(entity, new MoveSpeed());
            }
        }
    }
}