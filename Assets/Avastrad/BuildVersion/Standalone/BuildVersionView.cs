using TMPro;
using UnityEngine;

namespace Avastrad.BuildVersion.Standalone
{
    public class BuildVersionView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            var buildVersionHolder = BuildVersionHolderLoader.Load();
            tmpText.text = buildVersionHolder.GetCurrentVersionStr();
        }
    }
}