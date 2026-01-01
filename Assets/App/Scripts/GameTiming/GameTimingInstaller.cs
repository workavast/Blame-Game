using UnityEngine;
using Zenject;

namespace App.GameTiming
{
    public class GameTimingInstaller : MonoInstaller
    {
        [SerializeField] private GameTimeConfig config;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameTimer>().FromNew().AsSingle().WithArguments(config);
        }
    }
}