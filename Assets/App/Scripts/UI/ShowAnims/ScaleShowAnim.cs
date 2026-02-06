using System;
using DG.Tweening;
using UnityEngine;

namespace App.UI.ShowAnims
{
    [Serializable]
    public class ScaleShowAnim : ShowAnim
    {
        [SerializeField] protected RectTransform content;
        [SerializeField] protected Ease easingShowType = Ease.OutSine;
        [SerializeField] protected Vector3 directions = Vector3.up;
        [SerializeField, Min(0)] protected float showDuration = 0.1f;

        public override void Play() 
            => PlayScale(content);
        
        protected void PlayScale(Transform transform, Action onUpdate = null)
        {
            if (showDuration <= 0 || directions == Vector3.zero)
            {
                transform.localScale = Vector3.one;
                onUpdate?.Invoke();
                return;
            }

            var scale = transform.localScale;
            
            var tweenSequence = DOTween.Sequence();
            tweenSequence.SetUpdate(true);
            if (onUpdate != null) 
                tweenSequence.OnUpdate(() => onUpdate());

            if (directions.y > 0)
            {
                scale.y = 0;
                var tween = transform.DOScaleY(1, showDuration).SetEase(easingShowType);
                tweenSequence.Join(tween);                
            }

            if (directions.x > 0)
            {
                scale.x = 0;
                var tween = transform.DOScaleX(1, showDuration).SetEase(easingShowType);
                tweenSequence.Join(tween);
            }

            if (directions.z > 0)
            {
                scale.z = 0;
                var tween = transform.DOScaleZ(1, showDuration).SetEase(easingShowType);
                tweenSequence.Join(tween);
            }
            
            transform.localScale = scale;
            tweenSequence.Play();
        }
    }
}