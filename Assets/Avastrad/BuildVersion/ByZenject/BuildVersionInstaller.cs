using UnityEngine;
using Zenject;

namespace Avastrad.BuildVersion.ByZenject
{
    public class BuildVersionInstaller : MonoInstaller
    {
        [SerializeField] private bool showVersion = true;
        [SerializeField] private BuildVersionView viewPrefab;

        public override void InstallBindings()
        {
            var buildVersionHolder = BuildVersionHolderLoader.Load();
            Debug.Log(buildVersionHolder.GetCurrentVersionStr());
            if (!showVersion)
                return;
                    
            var view = Instantiate(viewPrefab);
            view.Initialize(buildVersionHolder);
        }
    }
}