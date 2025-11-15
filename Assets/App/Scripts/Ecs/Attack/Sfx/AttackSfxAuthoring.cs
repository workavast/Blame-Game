using App.Audio.Sources;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Ecs.Attack.Sfx
{
    public class AttackSfxAuthoring : MonoBehaviour
    {
        [SerializeField] private WeakObjectReference<AudioPoolRelease> sfxRef;

        private class Baker : Baker<AttackSfxAuthoring>
        {
            public override void Bake(AttackSfxAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new AttackSfxData()
                {
                    AttackSfxRef = authoring.sfxRef
                });

                AddComponent(entity, new AttackSfxInitializeFlag());
                AddComponent(entity, new AttackSfxHolderInitializeFlag());
                AddComponent(entity, new AttackSfxActivateFlag());
            }
        }
    }
}