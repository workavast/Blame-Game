using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Audio.Ambience.Effects
{
    public class AmbienceEffectUnloadTrigger<TAmbienceEffectsState> : MonoBehaviour
        where TAmbienceEffectsState : AmbienceEffectState
    {
        [Inject] private readonly TAmbienceEffectsState _ambienceEffectsState;
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private void Awake()
        {
            _sceneLoader.OnLoadingStarted += SceneUnloaded;
        }

        private void OnDestroy()
        {
            _sceneLoader.OnLoadingStarted -= SceneUnloaded;
        }

        private void SceneUnloaded()
        {
            if (_ambienceEffectsState.ApplyEffects) 
                _ambienceEffectsState.SetState(_ambienceEffectsState.ApplyEffects);
        }
    }
}