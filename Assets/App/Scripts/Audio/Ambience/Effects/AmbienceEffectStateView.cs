using UnityEngine;
using Zenject;

namespace App.Audio.Ambience.Effects
{
    public abstract class AmbienceEffectStateView<TAmbienceEffectsState> : MonoBehaviour
        where TAmbienceEffectsState : AmbienceEffectState
    {
        [SerializeField] private bool isReduceAudio; 
        [SerializeField] private int reduceVolumeRequestCount; 
        
        [Inject] private readonly TAmbienceEffectsState _ambienceEffectsState;

        private void Update()
        {
            isReduceAudio = _ambienceEffectsState.ApplyEffects;
            reduceVolumeRequestCount = _ambienceEffectsState.ApplyEffectsRequestCount;
        }
    }
}