using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Moving
{
    public class MoveAuthoring : MonoBehaviour
    {
        [SerializeField] private float defaultMoveSpeed;
        [SerializeField] private bool isPhysically;
        [SerializeField] private bool useDefaultMoveSystem = true;

        private class Baker : Baker<MoveAuthoring>
        {
            public override void Bake(MoveAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new MoveDirection());
                AddComponent(entity, new MoveSpeed() { Value = authoring.defaultMoveSpeed });

                if (authoring.isPhysically) 
                    AddComponent(entity, new PhysicsMassInitializeFlag());

                if (authoring.useDefaultMoveSystem) 
                    AddComponent(entity, new DefaultMoveTag());
            }
        }
    }
}