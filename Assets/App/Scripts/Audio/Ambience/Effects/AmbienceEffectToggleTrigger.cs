using UnityEngine;
using Zenject;

namespace App.Audio.Ambience.Effects
{
    public abstract class AmbienceEffectToggleTrigger<TAmbienceEffectsState> : MonoBehaviour
        where TAmbienceEffectsState : AmbienceEffectState
    {
        [Inject] private readonly TAmbienceEffectsState _ambienceEffectsState;

        private void OnEnable() 
            => _ambienceEffectsState.SetState(true);

        private void OnDisable() 
            => _ambienceEffectsState.SetState(false);
    }
}