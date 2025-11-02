using System;
using UnityEngine;

namespace App.Audio.Ambience.Effects
{
    public abstract class AmbienceEffectState
    {
        public bool ApplyEffects { get; private set; }
        public int ApplyEffectsRequestCount { get; private set; }

        public event Action OnStateChanged;

        public void SetState(bool applyEffects)
        {
            var prevValue = ApplyEffectsRequestCount;
            if (applyEffects)
                ApplyEffectsRequestCount++;
            else
                ApplyEffectsRequestCount--;

            if (ApplyEffectsRequestCount >= 1 && prevValue > 1)
                return;

            if (ApplyEffectsRequestCount < 0)
            {
                ApplyEffectsRequestCount = 0;
                Debug.LogWarning("You try reduce volume when it already done");
                return;
            }

            if (ApplyEffects != ApplyEffectsRequestCount > 0)
            {
                ApplyEffects = ApplyEffectsRequestCount > 0;
                OnStateChanged?.Invoke();
            }
        }
    }
}