using UnityEngine;
using Zenject;

namespace App.GameTiming
{
    public class GameTimingInstaller : MonoInstaller
    {
        [SerializeField] private float startTime;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameTimer>().FromNew().AsSingle().WithArguments(startTime);
        }
    }
}