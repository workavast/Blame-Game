using App.ScenesReferencing;
using Avastrad.ScenesLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.UI
{
    public class SceneLoadBtn : MonoBehaviour
    {
        [SerializeField] private Button btn;
        [SerializeField] private SceneReference gameplaySceneRef;
        [SerializeField] private LoadingConfig loadingConfig;
        
        [Inject] private readonly ISceneLoader _sceneLoader;

        private void Awake()
        {
            btn.onClick.AddListener(LoadScene);
        }

        private void LoadScene()
        {
            _sceneLoader.LoadScene(gameplaySceneRef.SceneIndex, loadingConfig.ShowDuration);
        }
    }
}