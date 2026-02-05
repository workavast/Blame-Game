using System;
using App.Ecs.EntityViews;
using DG.Tweening;
using UnityEngine;

namespace App.Ecs.Turrets
{
    public class TurretDisablingView : MonoBehaviour, IEntityViewElement
    {
        [SerializeField] private float targetHeight = -3f;
        [SerializeField] private float duration = 1f;
        [SerializeField] private Ease ease = Ease.Linear;
        
        public event Action<IEntityViewElement> OnCleanupCompleted;
        
        public bool OnDestroyCallback()
        {
            transform
                .DOMoveY(targetHeight, duration)
                .SetEase(ease)
                .SetLink(gameObject)
                .OnComplete(() => OnCleanupCompleted?.Invoke(this));
            
            return false;
        }
    }
}