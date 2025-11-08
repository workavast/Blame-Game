using TMPro;
using UnityEngine;

namespace Avastrad.BuildVersion.ByZenject
{
    public class BuildVersionView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;

        public void Initialize(BuildVersionHolder buildVersionHolder)
        {
            DontDestroyOnLoad(gameObject);
            
            tmpText.text = buildVersionHolder.GetCurrentVersionStr();
        }
    }
}