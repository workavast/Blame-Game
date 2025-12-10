using App.EscProviding;
using App.ScenesReferencing;
using Avastrad.ScenesLoading;
using Avastrad.UI.UiSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.UI
{
    public class GameplayMenu : ScreenBase, IEscListener
    {
        [SerializeField] private SceneReference mainMenuRef;
        [SerializeField] private SceneReference gameplayRef;
        [SerializeField] private LoadingConfig loadingConfig;
        [Space]
        [SerializeField] private Button continueBtn;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button backInMenuBtn;
        [SerializeField] private Button quitBtn;

        [Inject] private readonly ScreensController _screensController;
        [Inject] private readonly ISceneLoader _sceneLoader;
        [Inject] private readonly EscProvider _escProvider;

        public override void Initialize()
        {
            continueBtn.onClick.AddListener(ContinueGame);
            restartBtn.onClick.AddListener(RestartGame);
            backInMenuBtn.onClick.AddListener(BackInMenu);
            quitBtn.onClick.AddListener(QuitGame);
            _escProvider.Sub(this);
        }

        private void OnDestroy()
        {
            _escProvider.UnSub(this);
        }

        public void OnEscPressed() 
            => _screensController.ToggleScreen(GetType());

        private void RestartGame() 
            => _sceneLoader.LoadScene(gameplayRef.SceneIndex, loadingConfig.ShowDuration);

        private void ContinueGame() 
            => _screensController.ToggleScreen(GetType(), false);

        private void BackInMenu() 
            => _sceneLoader.LoadScene(mainMenuRef.SceneIndex, loadingConfig.ShowDuration);

        private void QuitGame() 
            => Application.Quit();
    }
}