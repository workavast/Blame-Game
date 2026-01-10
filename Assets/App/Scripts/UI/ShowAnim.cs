using System;
using DG.Tweening;
using UnityEngine;

namespace App.UI
{
    [Serializable]
    public abstract class ShowAnim
    {
        [SerializeField] protected Ease easingShowType = Ease.OutSine;
        [SerializeField, Min(0)] protected float showDuration = 0.1f;

        public abstract void Play(Transform transform);

        protected void PlayDefault(Transform transform, Action onUpdate = null)
        {
            if (showDuration <= 0)
            {
                transform.localScale = Vector3.one;
                onUpdate?.Invoke();
                return;
            }

            var scale = transform.localScale;
            scale.y = 0;
            transform.localScale = scale;

            var tween = transform.DOScaleY(1, showDuration).SetEase(easingShowType);
            tween.SetUpdate(true);
            if (onUpdate != null) 
                tween.OnUpdate(() => onUpdate());
            
            tween.Play();
        }
    }
}