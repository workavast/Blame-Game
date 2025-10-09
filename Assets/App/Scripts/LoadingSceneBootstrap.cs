using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App
{
    public class LoadingSceneBootstrap : MonoBehaviour
    {
        [Inject] private readonly ISceneLoader _sceneLoader;

        private void Start()
        {
            _sceneLoader.LoadTargetScene();
        }
    }
}