using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

namespace App.PlayerProviding
{
    public class HealthUiView : MonoBehaviour
    {
        [SerializeField] private Slider barView;
        [SerializeField] private TMP_Text txtView;
        [SerializeField] private Ease easing = Ease.OutSine;
        [SerializeField, Min(0f)] private float animDuration = 0.25f;

        [Inject] private readonly PlayerProvider _playerProvider;

        private Tweener _tween;
        private float _currentTweenTarget = -1f;

        private void OnDisable()
        {
            _tween?.Kill();
            _tween = null;
        }

        private void UpdateText()
        {
            if (txtView) 
                txtView.text = $"{_playerProvider.CurrentHealth}/{_playerProvider.MaxHealth}";
        }

        private void Update()
        {
            var targetValue = _playerProvider.FillPercentage;

            if (_tween != null && _tween.IsActive() && !_tween.IsComplete())
            {
                if (!Mathf.Approximately(_currentTweenTarget, targetValue))
                {
                    _currentTweenTarget = targetValue;
                    _tween.ChangeEndValue(targetValue, animDuration, true);
                }

                UpdateText();
                return;
            }

            if (!Mathf.Approximately(barView.value, targetValue))
            {
                if (animDuration <= 0f)
                {
                    barView.value = targetValue;
                    UpdateText();
                }
                else
                {
                    _currentTweenTarget = targetValue;
                    _tween = barView
                        .DOValue(targetValue, animDuration)
                        .SetEase(easing)
                        .SetUpdate(true)
                        .OnUpdate(UpdateText)
                        .OnComplete(() =>
                        {
                            UpdateText();
                            _tween = null;
                        });
                }
            }
        }
    }
}