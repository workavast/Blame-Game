using System;
using DG.Tweening;
using UnityEngine;

namespace App.UI.ShowAnims
{
    [Serializable]
    public class FadeShowAnim : ShowAnim
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] protected Ease easingShowType = Ease.OutSine;
        [SerializeField, Min(0)] protected float showDuration = 0.1f;
        
        public override void Play() 
            => PlayFadeAnim();

        private void PlayFadeAnim()
        {
            if (showDuration <= 0)
            {
                canvasGroup.alpha = 1;
                return;
            }
            
            canvasGroup.alpha = 0;
            canvasGroup
                .DOFade(1, showDuration)
                .SetEase(easingShowType);
        }
    }
}