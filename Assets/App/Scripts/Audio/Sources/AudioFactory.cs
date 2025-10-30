using System.Collections.Generic;
using Avastrad.PoolSystem.LiteDynamic;
using UnityEngine;
using Zenject;

namespace App.Audio.Sources
{
    public class AudioFactory
    {
        private readonly Transform _mainParent;
        private readonly DiContainer _diContainer;
        private readonly Dictionary<string, Transform> _parents = new();
        private readonly Dictionary<string, Pool<AudioSourceHolderPoolable>> _pools = new();
        
        public AudioFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;

            _mainParent = new GameObject { transform = { name = "Audio" } }.transform;
        }

        public TSource Create<TSource>(TSource prefab, Vector3 position)
            where TSource : AudioSourceHolderPoolable
        {
            var key = prefab.gameObject.name;
            return Create(key, prefab, position);
        }
        
        public TSource Create<TSource>(TSource prefab, Vector3 position, Vector3 pitchRange) 
            where TSource : AudioSourceHolderPoolable
            => Create(prefab, position, pitchRange.x, pitchRange.y);

        public TSource Create<TSource>(TSource prefab, Vector3 position, float minPitch, float maxPitch)
            where TSource : AudioSourceHolderPoolable
        {
            var pitch = Random.Range(minPitch, maxPitch);
            return Create(prefab, position, pitch);
        }
        
        public TSource Create<TSource>(TSource prefab, Vector3 position, float pitch)
            where TSource : AudioSourceHolderPoolable
        {
            var key = prefab.gameObject.name;
            var audioSource = Create(key, prefab, position);
            audioSource.SetPitch(pitch);
            return audioSource;
        }

        private TSource Create<TSource>(string key, TSource prefab, Vector3 position)
            where TSource : AudioSourceHolderPoolable
        {
            if (!_pools.ContainsKey(key))
            {
                var parent = new GameObject { transform = { name = key , parent = _mainParent} }.transform;
                _parents.Add(key, parent);
                
                _pools.Add(key, new Pool<AudioSourceHolderPoolable>());
            }
            
            var element = _pools[key].ExtractElement(() => InstantiateEntity(prefab, key));
            element.transform.position = position;

            return element as TSource;
        }

        private AudioSourceHolderPoolable InstantiateEntity(AudioSourceHolderPoolable prefab, string key)
        {
            var someAudioSource = _diContainer.InstantiatePrefab(prefab, _parents[key]).GetComponent<AudioSourceHolderPoolable>();
            return someAudioSource;
        }
    }
}