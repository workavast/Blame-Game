using App.ScenesReferencing;
using App.UI;
using Avastrad.ScenesLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.GameEndDetection
{
    public class GameLooseUi : DefaultScreen
    {
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button backInMenuBtn;
        [SerializeField] private SceneReference gameplaySceneRef;
        [SerializeField] private SceneReference mainMenuSceneRef;
        [SerializeField] private LoadingConfig loadingConfig;
        
        [Inject] private readonly ISceneLoader _sceneLoader;

        public override void Initialize()
        {
            restartBtn?.onClick.AddListener(Restart);
            backInMenuBtn?.onClick.AddListener(LoadMenu);
            base.Initialize();
        }

        private void Restart() 
            => _sceneLoader.LoadScene(gameplaySceneRef.SceneIndex, loadingConfig.ShowDuration);

        private void LoadMenu() 
            => _sceneLoader.LoadScene(mainMenuSceneRef.SceneIndex, loadingConfig.ShowDuration);
    }
}