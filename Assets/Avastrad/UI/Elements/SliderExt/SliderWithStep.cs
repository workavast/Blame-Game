using System;
using UnityEngine;
using UnityEngine.UI;

namespace Avastrad.UI.Elements.SliderExt
{
    public class SliderWithStep : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField, Min(0)] private float step;
        
        public float Value => slider.value * step;
        
        public event Action<float> OnValueChanged;
        public event Action<float> OnValueViewChanged;
        
        private void Awake()
        {
            slider.onValueChanged.AddListener(OnSlideValueChanged);
        }

        public void SetValue(float value, bool notify = true)
        {
            value = Mathf.Round(value / step) * step;
            
            if (notify)
                slider.value = value / step;
            else
            {
                slider.SetValueWithoutNotify(value / step);
                OnValueViewChanged?.Invoke(value);
            }
        }

        public void SetMinValue(float minValue)
        {
            slider.minValue = minValue / step;
        }
        
        public void SetMaxValue(float maxValue)
        {
            slider.maxValue = maxValue / step;
        }
        
        private void OnSlideValueChanged(float value)
        {
            value *= step;
            OnValueChanged?.Invoke(value);
            OnValueViewChanged?.Invoke(value);
        }
    }
}