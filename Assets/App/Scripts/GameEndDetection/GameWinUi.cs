using App.ScenesReferencing;
using Avastrad.ScenesLoading;
using Avastrad.UI.UiSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.GameEndDetection
{
    public class GameWinUi : ScreenBase
    {
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button backInMenuBtn;
        [SerializeField] private SceneReference gameplaySceneRef;
        [SerializeField] private SceneReference mainMenuSceneRef;

        [Inject] private readonly ISceneLoader _sceneLoader;

        public override void Initialize()
        {
            restartBtn?.onClick.AddListener(Restart);
            backInMenuBtn?.onClick.AddListener(LoadMenu);
            base.Initialize();
        }

        private void Restart() 
            => _sceneLoader.LoadScene(gameplaySceneRef.SceneIndex);

        private void LoadMenu() 
            => _sceneLoader.LoadScene(mainMenuSceneRef.SceneIndex);
    }
}