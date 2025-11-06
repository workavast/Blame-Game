using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Avastrad.BuildVersion.Editor
{
    public class PostprocessBuildVersionChanger : IPostprocessBuildWithReport
    {
        public int callbackOrder => 1;
        
        public void OnPostprocessBuild(BuildReport report)
        {
            var buildVersionHolder = BuildVersionHolderLoader.Load();
            buildVersionHolder.PrevGameVersion = Application.version;
            BuildVersionHolderLoader.Save(buildVersionHolder, false);
        }
    }
}