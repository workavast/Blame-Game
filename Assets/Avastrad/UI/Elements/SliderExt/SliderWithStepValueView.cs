using System.Globalization;
using TMPro;
using UnityEngine;

namespace Avastrad.UI.Elements.SliderExt
{
    public class SliderWithStepValueView : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueView;
        [SerializeField] private SliderWithStep sliderWithStep;

        private void OnEnable()
        {
            sliderWithStep.OnValueViewChanged += UpdateView;
            UpdateView(sliderWithStep.Value);
        }

        private void OnDisable() 
            => sliderWithStep.OnValueViewChanged -= UpdateView;

        private void UpdateView(float value) 
            => valueView.text = value.ToString(CultureInfo.InvariantCulture);
    }
}