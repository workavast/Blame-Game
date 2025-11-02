using UnityEngine;
using Zenject;

namespace App.Audio.Ambience.Effects.LowPass
{
    public class LowPassToggler : AmbienceEffectorToggler
    {
        [SerializeField] private float defaultLowPass;
        [SerializeField] private float targetLowPassValue;
        [SerializeField] private float transitionTime = 1;
        
        [Inject] private readonly AmbienceManager _ambienceManager;
        [Inject] private readonly LowPassEffectState _effectState;

        private LowPassFilterEffector _lowPassFilterEffector;

        public override void Init()
        {
            _effectState.OnStateChanged += Toggle;
            Toggle();
        }

        private void OnDestroy()
        {
            _effectState.OnStateChanged -= Toggle;
        }

        private void Toggle()
        {
            if (_effectState.ApplyEffects)
            {
                _lowPassFilterEffector = new LowPassFilterEffector(targetLowPassValue, defaultLowPass, transitionTime);
                _ambienceManager.ApplyEffect(_lowPassFilterEffector);
            }
            else
            {
                if (_lowPassFilterEffector != null)
                    _ambienceManager.RevertEffect(_lowPassFilterEffector);
            }
        }
    }
}