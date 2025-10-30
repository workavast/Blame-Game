using App.Audio;
using App.Audio.Sources;
using App.Ecs.Clenuping;
using Unity.Entities.Content;
using UnityEngine;
using Zenject;

namespace App.Ecs.Shooting
{
    public class ShooterSfxView : CleanupView
    {
        [SerializeField] private Vector2 pitchRange = new(0.9f, 1.1f);

        private SfxHolder _shootSfx;
        
        [Inject]
        public void Construct(AudioFactory audioFactory)
        {
            _shootSfx = new SfxHolder(audioFactory);
        }

        protected override void OnDestroy()
        {
            _shootSfx.ReleaseIfUnused();
            base.OnDestroy();
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