using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Avastrad.UI.Elements.SliderExt
{
    public class SliderValueView : MonoBehaviour
    {
        [SerializeField] private TMP_Text textView;
        [SerializeField] private string format = "F1";
        [SerializeField] private Slider slider;

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(UpdateView);
            UpdateView(slider.value);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(UpdateView);
        }

        private void UpdateView(float value)
        {
            textView.text = value.ToString(format, CultureInfo.InvariantCulture);
        }
    }
}