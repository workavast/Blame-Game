using System;

namespace Avastrad.ScenesLoading
{
    public interface ISceneLoader
    {
        public int PrevTargetSceneIndex { get; }

        public event Action OnLoadingStarted;
        public event Action OnLoadingScreenHided;

        public void ShowLoadScreen(bool showInstantly, Action onShowedCallback = null);
        public void HideLoadScreen(bool hideLoadScreenInstantly);
        public void LoadScene(int index, bool showLoadScreenInstantly = false, bool skipLoadingScreen = false);
        public void LoadTargetScene();
    }
}