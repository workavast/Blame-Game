using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Avastrad.BuildVersion.Editor
{
    public class PreprocessBuildVersionChanger : IPreprocessBuildWithReport
    {
        public int callbackOrder => 1;
        
        public void OnPreprocessBuild(BuildReport report)
        {
            var buildVersionHolder = BuildVersionHolderLoader.Load();

            if (Application.version == buildVersionHolder.PrevGameVersion)
            {
                buildVersionHolder.BuildVersion++;
                Debug.LogWarning("You doesnt change game version");
            }
            else
            {
                buildVersionHolder.BuildVersion = 0;
            }
            
            BuildVersionHolderLoader.Save(buildVersionHolder, true);
        }
    }
}