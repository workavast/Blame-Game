using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

namespace App.LevelManagement.ExpManagement
{
    public class PlayerExpUiSliderView : MonoBehaviour
    {
        [SerializeField] private Slider expBarView;
        [SerializeField] private Ease easing = Ease.OutSine;
        [SerializeField, Min(0f)] private float animDuration = 0.25f;

        [Inject] private readonly IExpStorageRO _expStorage;

        private Tweener _tween;
        private float _currentTweenTarget = -1f;

        private void OnDisable()
        {
            _tween?.Kill();
            _tween = null;
        }

        private void Update()
        {
            var target = _expStorage.FillTargetPercentage;

            if (_tween != null && _tween.IsActive() && !_tween.IsComplete())
            {
                if (!Mathf.Approximately(_currentTweenTarget, target))
                {
                    _currentTweenTarget = target;
                    _tween.ChangeEndValue(target, animDuration, true);
                }

                return;
            }

            if (!Mathf.Approximately(expBarView.value, target))
            {
                if (animDuration <= 0f)
                {
                    expBarView.value = target;
                }
                else
                {
                    _currentTweenTarget = target;
                    _tween = expBarView
                        .DOValue(target, animDuration)
                        .SetEase(easing)
                        .SetUpdate(true)
                        .OnComplete(() => _tween = null);
                }
            }
        }
    }
}