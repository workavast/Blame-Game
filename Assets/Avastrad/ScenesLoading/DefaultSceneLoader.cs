using System;
using UnityEngine.SceneManagement;

namespace Avastrad.ScenesLoading
{
    public class DefaultSceneLoader : ISceneLoader
    {
        private const int InitialIndex = -1;
        
        private readonly int _loadingSceneIndex;
        private readonly ILoadingScreen _loadingScreen;
        
        private int _targetSceneIndex = InitialIndex;

        public event Action OnLoadingStarted;
        public event Action OnLoadingScreenHided;

        public DefaultSceneLoader(ILoadingScreen loadingScreen, int loadingSceneIndex)
        {
            _loadingScreen = loadingScreen;
            _loadingSceneIndex = loadingSceneIndex;
            
            _loadingScreen.OnHided += () => OnLoadingScreenHided?.Invoke();
        }
        
        public void ShowLoadScreen(float duration, Action onShowedCallback)
            => _loadingScreen.Show(duration, onShowedCallback);
        
        public void HideLoadScreen(float duration)
            => _loadingScreen.Hide(duration);

        public void LoadScene(int index, float duration, bool skipLoadingScene = false)
        {
            _targetSceneIndex = index;
            
            OnLoadingStarted?.Invoke();

            if (skipLoadingScene)
                _loadingScreen.Show(duration, () => SceneManager.LoadSceneAsync(_targetSceneIndex));
            else
                _loadingScreen.Show(duration, () => SceneManager.LoadSceneAsync(_loadingSceneIndex));
        }

        public void LoadTargetScene() 
            => SceneManager.LoadSceneAsync(_targetSceneIndex);
    }
}