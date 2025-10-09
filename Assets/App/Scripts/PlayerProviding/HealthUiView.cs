using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.PlayerProviding
{
    public class HealthUiView : MonoBehaviour
    {
        [SerializeField] private Slider barView;
        [SerializeField] private TMP_Text txtView;

        [Inject] private readonly PlayerProvider _playerProvider;

        private void Update()
        {
            var currentFillPercentage = _playerProvider.FillPercentage;
            if (!Mathf.Approximately(barView.value, currentFillPercentage))
            {
                barView.value = currentFillPercentage;
                txtView.text = $"{_playerProvider.CurrentHealth}/{_playerProvider.MaxHealth}";
            }
        }
    }
}