using App.Ecs.Shooting.Ammo;
using DG.Tweening;
using UnityEngine;

namespace App.Ecs.Turrets
{
    public class TurretCapacityView : MonoBehaviour
    {
        [SerializeField] private AmmoCapacityView ammoCapacityView;
        [SerializeField] private TurretSphereView sphereView;
        [SerializeField] private Ease percentageEase = Ease.InQuint;
        [SerializeField] private Ease animationEase = Ease.Linear;
        [SerializeField] private float duration = 0.2f;

        private float _lastPercentage = 1;
        private Tween _currentTween;

        private void Awake()
        {
            ammoCapacityView.OnCapacityPercentageChanged += SetCapacityPercentage;
        }

        private void SetCapacityPercentage(float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            var targetPercentage = DOVirtual.EasedValue(0f, 1f, percentage, animationEase);

            _currentTween?.Kill();

            _currentTween = DOTween.To(() => _lastPercentage, x => _lastPercentage = x, targetPercentage, duration)
                .SetEase(animationEase)
                .SetLink(gameObject)
                .OnUpdate(() => sphereView.SetScale(_lastPercentage))
                .OnComplete(() => _currentTween = null);
        }
    }
}