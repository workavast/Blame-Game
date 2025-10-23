using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Avastrad.ScenesLoading
{
    public class DefaultSceneLoader : ISceneLoader
    {
        private const int InitialIndex = -1;
        
        private readonly int _loadingSceneIndex;
        private readonly ILoadingScreen _loadingScreen;
        
        private int _targetSceneIndex = InitialIndex;

        public int PrevTargetSceneIndex { get; private set; } = InitialIndex;
        
        public event Action OnLoadingStarted;
        public event Action OnLoadingScreenHided;

        public DefaultSceneLoader(ILoadingScreen loadingScreen, int loadingSceneIndex)
        {
            _loadingScreen = loadingScreen;
            _loadingSceneIndex = loadingSceneIndex;
            
            _loadingScreen.OnHided += () => OnLoadingScreenHided?.Invoke();
        }
        
        public void ShowLoadScreen(bool showInstantly, Action onShowedCallback)
            => _loadingScreen.Show(showInstantly, onShowedCallback);
        
        public void HideLoadScreen(bool hideLoadScreenInstantly)
            => _loadingScreen.Hide(hideLoadScreenInstantly);

        public void LoadScene(int index, bool showLoadScreenInstantly = false, bool skipLoadingScreen = false)
        {
            PrevTargetSceneIndex = _targetSceneIndex;
            _targetSceneIndex = index;
            
            OnLoadingStarted?.Invoke();

            if (skipLoadingScreen)
                SceneManager.LoadScene(_loadingSceneIndex);
            else
                _loadingScreen.Show(showLoadScreenInstantly, () => SceneManager.LoadSceneAsync(_loadingSceneIndex));
        }

        public void LoadTargetScene() 
            => SceneManager.LoadSceneAsync(_targetSceneIndex);
    }
}