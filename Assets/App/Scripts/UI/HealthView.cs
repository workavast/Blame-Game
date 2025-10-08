using App.PlayerProviding;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider expBarView;
        [SerializeField] private TMP_Text expTxtView;

        [Inject] private readonly PlayerProvider _playerProvider;

        private void Update()
        {
            var currentFillPercentage = _playerProvider.FillPercentage;
            if (!Mathf.Approximately(expBarView.value, currentFillPercentage))
            {
                expBarView.value = currentFillPercentage;
                expTxtView.text = $"{_playerProvider.CurrentHealth}/{_playerProvider.MaxHealth}";
            }
        }
    }
}