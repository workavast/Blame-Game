using System;

namespace Avastrad.ScenesLoading
{
    public interface ILoadingScreen
    {
        public bool IsVisible { get; }

        public event Action OnHided;
        
        public void Show(float duration, Action onShowedCallback);
        public void Hide(float duration);
    }
}