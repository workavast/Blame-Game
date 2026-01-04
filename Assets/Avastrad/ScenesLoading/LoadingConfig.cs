using UnityEngine;

namespace Avastrad.ScenesLoading
{
    [CreateAssetMenu(fileName = nameof(LoadingConfig), menuName = "App/" + nameof(LoadingConfig))] 
    public class LoadingConfig : ScriptableObject
    {
        [SerializeField] private float showDuration;
        [SerializeField] private float hideDuration;

        public float ShowDuration => showDuration;
        public float HideDuration => hideDuration;
    }
}