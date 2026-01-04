using System;

namespace Avastrad.ScenesLoading
{
    public interface ISceneLoader
    {
        public event Action OnLoadingStarted;
        public event Action OnLoadingScreenHided;

        public void ShowLoadScreen(float duration, Action onShowedCallback = null);
        public void HideLoadScreen(float duration);
        public void LoadScene(int index, float duration, bool skipLoadingScene = false);
        public void LoadTargetScene();
    }
}