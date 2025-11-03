using System.Globalization;
using TMPro;
using UnityEngine;

namespace Avastrad.UI.Elements.SliderExt
{
    public class SliderProviderValueView : MonoBehaviour
    {
        [SerializeField] private TMP_Text textView;
        [SerializeField] private SliderProvider slider;

        private void OnEnable()
        {
            slider.OnValueChanged += UpdateView;
            UpdateView(slider.Value);
        }

        private void OnDisable()
        {
            slider.OnValueChanged -= UpdateView;
        }

        private void UpdateView(float value)
        {
            textView.text = value.ToString("F1", CultureInfo.InvariantCulture);
        }
    }
}