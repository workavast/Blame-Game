using UnityEngine;
using Zenject;

namespace App.PostVfx
{
    public class PosVfxInstaller : MonoInstaller
    {
        [SerializeField] private Camera postVfxCamera;
        [SerializeField] private RenderTexture renderTexture;
        
        public override void InstallBindings()
        {
            Container.Bind<PostVfxResizer>().FromNew().AsSingle().WithArguments(postVfxCamera, renderTexture).NonLazy();
        }
    }
}