using System.Collections;
using UnityEngine;

namespace App.Audio.Ambience.Effects.LowPass
{
    public class LowPassFilterEffector : IAmbientEffector
    {
        private readonly float _targetLowPassValue;
        private readonly float _defaultLowPassValue;
        private readonly float _transitionTime;

        private Coroutine _lastCoroutine;
        private AmbienceSource _ambienceSource;
        private float _timer;
        
        public LowPassFilterEffector(float targetTargetLowPassValue, float defaultLowPassValue, float transitionTime)
        {
            _targetLowPassValue = targetTargetLowPassValue;
            _defaultLowPassValue = defaultLowPassValue;
            _transitionTime = transitionTime;
        }

        public bool IsSameAmbienceSource(AmbienceSource ambienceSource) 
            => _ambienceSource == ambienceSource;

        public void Apply(AmbienceSource ambienceSource)
        {
            if (_ambienceSource != null)
            {
                Debug.LogError($"Effector already used: prev source[{_ambienceSource.name}] new source [{ambienceSource.name}]");
                return;
            }

            _ambienceSource = ambienceSource;
            if (ambienceSource.TryGetComponent<AudioLowPassFilter>(out var lowPass))
                StartTransition(lowPass, _targetLowPassValue, _defaultLowPassValue);
            else
                Debug.LogError($"Ambient doesnt have low pass filter: [{ambienceSource.name}]");
        }

        public void Revert(AmbienceSource ambienceSource)
        {
            if (!IsSameAmbienceSource(ambienceSource))
            {
                Debug.LogError($"You try revert effect from different ambience source: initial ambience [{_ambienceSource}], active ambience [{ambienceSource.name}]");
                return;
            }

            if (ambienceSource.TryGetComponent<AudioLowPassFilter>(out var lowPass))
                StartTransition(lowPass, _defaultLowPassValue, _targetLowPassValue);
            else
                Debug.LogError($"Ambient doesnt have low pass filter: [{ambienceSource.name}]");
        }

        private void StartTransition(AudioLowPassFilter lowPassFilter, float targetValue, float initialValue)
        {
            if (_lastCoroutine != null)
                _ambienceSource.StopCoroutine(_lastCoroutine);
            
            _lastCoroutine = _ambienceSource.StartCoroutine(PerformTransition(lowPassFilter, targetValue, initialValue));
        }
        
        private IEnumerator PerformTransition(AudioLowPassFilter lowPassFilter, float targetValue, float initialValue)
        {
            _timer = 0;
            do
            {
                _timer += Time.unscaledDeltaTime;
                lowPassFilter.cutoffFrequency = Mathf.Lerp(initialValue, targetValue, _timer / _transitionTime);
                yield return new WaitForEndOfFrame();
            } while (_timer < _transitionTime);
        }
    }
}