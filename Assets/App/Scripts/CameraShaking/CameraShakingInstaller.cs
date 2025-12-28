using UnityEngine;
using Zenject;

namespace App.CameraShaking
{
    public class CameraShakingInstaller : MonoInstaller
    {
        [SerializeField] private CameraShakeBehaviour cameraShakeBehaviour;
        
        public override void InstallBindings()
        {
            Container.Bind<CameraShakeBehaviour>().FromInstance(cameraShakeBehaviour).AsSingle();
            Container.Bind<CameraShakeSettingProvider>().FromNew().AsSingle();
        }
    }
}