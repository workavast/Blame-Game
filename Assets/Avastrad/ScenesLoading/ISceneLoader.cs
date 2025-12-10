using System;

namespace Avastrad.ScenesLoading
{
    public interface ISceneLoader
    {
        public int PrevTargetSceneIndex { get; }

        public event Action OnLoadingStarted;
        public event Action OnLoadingScreenHided;

        public void ShowLoadScreen(float duration, Action onShowedCallback = null);
        public void HideLoadScreen(float duration);
        public void LoadScene(int index, float duration, bool skipLoadingScreen = false);
        public void LoadTargetScene();
    }
}