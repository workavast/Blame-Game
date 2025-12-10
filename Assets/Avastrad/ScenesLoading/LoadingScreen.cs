using System;
using System.Collections;
using UnityEngine;

namespace Avastrad.ScenesLoading
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        public bool IsVisible { get; private set; }
        
        public event Action OnPreShow;
        public event Action OnHided;

        public void Initialize() 
            => IsVisible = gameObject.activeSelf;

        public void Show(float duration, Action onShowedCallback)
        {
            OnPreShow?.Invoke();
            if (duration <= 0)
                ShowInstantly(onShowedCallback);
            else
                ShowWithFade(duration, onShowedCallback);
        }

        public void Hide(float duration)
        {
            if (duration <= 0)
                HideInstantly();
            else
                HideWithFade(duration);
        }

        private void ShowInstantly(Action onShowedCallback)
        {
            StopAllCoroutines();
            canvasGroup.alpha = 1;
            gameObject.SetActive(true);
            IsVisible = true;
            onShowedCallback?.Invoke();
        }
        
        private void ShowWithFade(float duration, Action onShowedCallback)
        {
            StopAllCoroutines();
            IsVisible = true;
            gameObject.SetActive(true);
            StartCoroutine(ShowFade(duration, onShowedCallback));
        }
        
        private void HideInstantly()
        {
            StopAllCoroutines();
            IsVisible = false;
            gameObject.SetActive(false);
            OnHided?.Invoke();
        }
        
        private void HideWithFade(float duration)
        {
            StopAllCoroutines();
            
            if (!IsVisible)
                return;

            StartCoroutine(HideFade(duration));
        }
        
        private IEnumerator ShowFade(float duration, Action onShowedCallback)
        {
            float timer = 0;

            while (timer < duration)
            {
                yield return new WaitForEndOfFrame();
                canvasGroup.alpha = timer/duration;
                timer += Time.unscaledDeltaTime;
            }
            
            canvasGroup.alpha = 1;
            onShowedCallback?.Invoke();
        }

        private IEnumerator HideFade(float duration)
        {
            float timer = 0;

            while (timer < duration)
            {
                yield return new WaitForEndOfFrame();
                canvasGroup.alpha = 1 - timer/duration;
                timer += Time.unscaledDeltaTime;
            }
            
            IsVisible = false;
            canvasGroup.alpha = 0;
            gameObject.SetActive(false);
            OnHided?.Invoke();
        }
    }
}