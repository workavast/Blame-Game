using System.Threading.Tasks;
using App.LevelUpManagement;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App
{
    public class GameplayBootstrap : MonoBehaviour
    {
        [Inject] private readonly LevelUpManager _levelUpManager;
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private async void Start()
        {
            _levelUpManager.LevelUp();

            await Task.Delay(2000);
            _sceneLoader.HideLoadScreen(false);
        }
    }
}