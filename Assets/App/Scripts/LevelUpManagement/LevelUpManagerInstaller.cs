using App.Perks;
using UnityEngine;
using Zenject;

namespace App.LevelUpManagement
{
    public class LevelUpManagerInstaller : MonoInstaller
    {
        [SerializeField] private PerksChooseWindow perksChooseWindow;
        
        public override void InstallBindings()
        {
            Container.Bind<LevelUpManager>().FromNew().AsSingle().WithArguments(perksChooseWindow);
        }
    }
}