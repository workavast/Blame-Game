using UnityEngine;

namespace Avastrad.BuildVersion
{
    public class BuildVersionHolder
    {
        public string PrevGameVersion;
        public int BuildVersion = 0;

        public BuildVersionHolder()
        {
            BuildVersion = 0;
        }

        public string GetCurrentVersionStr() 
            => $"V-{Application.version}-{BuildVersion}";
    }
}