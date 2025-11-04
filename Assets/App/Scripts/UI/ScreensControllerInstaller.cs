using Avastrad.UI.UiSystem;
using UnityEngine;
using Zenject;

namespace App.UI
{
    public class ScreensControllerInstaller : MonoInstaller
    {
        [SerializeField] private ScreensController screensController;
        
        public override void InstallBindings()
        {
            Container.Bind<ScreensController>().FromInstance(screensController).AsSingle();
        }
    }
}