using App.ScenesReferencing;
using Avastrad.ScenesLoading;
using Avastrad.UI.UiSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.UI
{
    public class MainMenu : ScreenBase
    {
        [SerializeField] private Button startGameBtn;
        [SerializeField] private Button quitBtn;
        [SerializeField] private SceneReference gameplaySceneRef;
        [SerializeField] private LoadingConfig loadingConfig;

        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private void Awake()
        {
            startGameBtn.onClick.AddListener(StartGame);
            quitBtn.onClick.AddListener(Quit);
        }

        private void StartGame()
        {
            _sceneLoader.LoadScene(gameplaySceneRef.SceneIndex, loadingConfig.ShowDuration);
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}