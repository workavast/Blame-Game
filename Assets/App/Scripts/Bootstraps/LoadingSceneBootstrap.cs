using App.Audio.Ambience;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Bootstraps
{
    public class LoadingSceneBootstrap : MonoBehaviour
    {
        [SerializeField] private AmbienceBootstrap ambienceBootstrap;
        
        [Inject] private readonly ISceneLoader _sceneLoader;

        private void Start()
        {
            ambienceBootstrap.Initialize();
            
            _sceneLoader.LoadTargetScene();
        }
    }
}