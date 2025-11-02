using System;

namespace App.Audio.Ambience.Effects
{
    public abstract class AmbienceEffectState
    {
        public bool ApplyEffect => _requestCounter.RequestMoreZero;
        public int ApplyEffectRequestCount => _requestCounter.RequestsCount;

        private readonly RequestCounter _requestCounter;
        
        public event Action OnStateChanged;

        public AmbienceEffectState()
        {
            _requestCounter = new RequestCounter(PerformChange);
        }
        
        public void SetState(bool applyEffects) 
            => _requestCounter.ChangeRequests(applyEffects);

        private void PerformChange(bool applyEffects) 
            => OnStateChanged?.Invoke();
    }
}