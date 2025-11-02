using System.Threading.Tasks;
using App.Audio.Ambience;
using App.LevelManagement;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Bootstraps
{
    public class GameplayBootstrap : MonoBehaviour
    {
        [SerializeField] private AmbienceBootstrap ambienceBootstrap;
        
        [Inject] private readonly LevelStorage _levelStorage;
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private async void Start()
        {
            ambienceBootstrap.Initialize();
            
            _levelStorage.LevelUp();

            await Task.Delay(2000);
            _sceneLoader.HideLoadScreen(false);
        }
    }
}