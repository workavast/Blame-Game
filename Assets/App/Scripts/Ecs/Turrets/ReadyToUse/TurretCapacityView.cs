using System;
using App.Ecs.EntityViews;
using DG.Tweening;
using UnityEngine;

namespace App.Ecs.Turrets.ReadyToUse
{
    public class TurretCapacityView : MonoBehaviour, IEntityViewElement
    {
        [SerializeField] private TurretSphereView sphereView;
        [SerializeField] private Ease percentageEase = Ease.InQuint;
        [SerializeField] private Ease animationEase = Ease.Linear;
        [SerializeField] private float duration = 0.2f;

        private float _lastPercentage = 1;
        private Tween _currentTween;

        public event Action<IEntityViewElement> OnCleanupCompleted;

        public bool OnDestroyCallback() 
            => true;

        public void SetCapacityPercentage(float percentage)
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