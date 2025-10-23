using System.Threading.Tasks;
using App.LevelManagement;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Bootstraps
{
    public class GameplayBootstrap : MonoBehaviour
    {
        [Inject] private readonly LevelStorage _levelStorage;
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private async void Start()
        {
            _levelStorage.LevelUp();

            await Task.Delay(2000);
            _sceneLoader.HideLoadScreen(false);
        }
    }
}