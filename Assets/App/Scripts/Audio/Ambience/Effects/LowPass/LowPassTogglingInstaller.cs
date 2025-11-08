using Zenject;

namespace App.Audio.Ambience.Effects.LowPass
{
    public class LowPassTogglingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LowPassEffectState>().FromNew().AsSingle();
        }
    }
}