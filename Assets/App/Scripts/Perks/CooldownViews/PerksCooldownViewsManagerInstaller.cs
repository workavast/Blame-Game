using UnityEngine;
using Zenject;

namespace App.Perks.CooldownViews
{
    public class PerksCooldownViewsManagerInstaller : MonoInstaller
    {
        [SerializeField] private UiPerksCooldownViews uiPerksCooldownViews;
        
        public override void InstallBindings()
        {
            Container.Bind<UiPerksCooldownViews>().FromInstance(uiPerksCooldownViews).AsSingle();
        }
    }
}