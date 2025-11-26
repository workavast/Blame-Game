using System;
using App.Audio.Sources;
using App.Ecs.EntityViews;
using Unity.Entities.Content;
using UnityEngine;
using Zenject;

namespace App.Ecs.Shooting
{
    public class ShooterSfxView : MonoBehaviour, IEntityViewElement
    {
        [SerializeField] private Vector2 pitchRange = new(0.9f, 1.1f);

        private SfxHolder _shootSfx;
        public event Action<IEntityViewElement> OnCleanupCompleted;

        [Inject]
        public void Construct(AudioFactory audioFactory)
        {
            _shootSfx = new SfxHolder(audioFactory);
        }

        public bool OnDestroyCallback() 
            => true;

        private void OnDestroy()
        {
            _shootSfx.ReleaseIfUnused();
        }

        public void PlaySfx(Vector3 position)
        {
            _shootSfx.Play(position, pitchRange);
        }

        public void SetShootSfx(WeakObjectReference<AudioPoolRelease> shootSfxRef)
        {
            _shootSfx.SetSfx(shootSfxRef);
        }
    }
}