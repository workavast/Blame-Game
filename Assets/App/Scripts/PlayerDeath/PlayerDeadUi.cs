using App.ScenesReferencing;
using Avastrad.ScenesLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.PlayerDeath
{
    public class PlayerDeadUi : MonoBehaviour
    {
        [SerializeField] private Button backInMenuBtn;
        [SerializeField] private SceneReference mainMenuSceneRef;

        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private void Awake()
        {
            backInMenuBtn?.onClick.AddListener(LoadMenu);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void LoadMenu()
        {
            _sceneLoader.LoadScene(mainMenuSceneRef.SceneIndex);
        }
    }
}