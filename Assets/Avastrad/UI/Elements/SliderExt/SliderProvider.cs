using System;
using UnityEngine;
using UnityEngine.UI;

namespace Avastrad.UI.Elements.SliderExt
{
    [RequireComponent(typeof(Slider))]
    public class SliderProvider : MonoBehaviour
    {
        public float Value => _slider.value;
        public float MaxValue => _slider.maxValue;
        public float MinValue => _slider.minValue;

        public event Action OnChanged;
        public event Action<float> OnMaxValueChanged;
        public event Action<float> OnMinValueChanged;
        public event Action<float> OnValueChanged;
        
        private Slider _slider;
        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void SetValue(float value, bool notify = true)
        {
            _slider.value = value;
            OnValueChanged?.Invoke(Value);
            OnChanged?.Invoke();
        }
        
        public void SetMaxValue(float value, bool notify = true)
        {
            _slider.maxValue = value;
            OnMaxValueChanged?.Invoke(MaxValue);
            OnChanged?.Invoke();
        }
        
        public void SetMinValue(float value, bool notify = true)
        {
            _slider.minValue = value;
            OnMinValueChanged?.Invoke(MinValue);
            OnChanged?.Invoke();
        }
    }
}