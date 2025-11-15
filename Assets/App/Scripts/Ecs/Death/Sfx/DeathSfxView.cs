using App.Audio.Sources;
using Unity.Entities.Content;
using UnityEngine;
using Zenject;

namespace App.Ecs.Death.Sfx
{
    public class DeathSfxView : MonoBehaviour
    {
        [SerializeField] private Vector2 deathPitchRange;

        private SfxHolder _sfxHolder;

        [Inject]
        public void Construct(AudioFactory audioFactory)
        {
            _sfxHolder = new SfxHolder(audioFactory);
        }
        
        protected void OnDestroy()
        {
            _sfxHolder.ReleaseIfUnused();
        }

        public void SetDeathSfx(WeakObjectReference<AudioPoolRelease> deathSfxRef)
        {
            _sfxHolder.SetSfx(deathSfxRef);
        }
        
        public void Activate()
        {
            _sfxHolder.Play(transform.position, deathPitchRange);
        }
    }
}