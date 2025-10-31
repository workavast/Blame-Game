using UnityEngine;
using Zenject;

namespace App.MonoBehProviding
{
    public class MonoBehProvidingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var monoBehProvider = new GameObject() { name = nameof(MonoBehProvider) }.AddComponent<MonoBehProvider>();
            DontDestroyOnLoad(monoBehProvider);
            
            Container.BindInterfacesTo<MonoBehProvider>().FromInstance(monoBehProvider).AsSingle();
        }
    }
}