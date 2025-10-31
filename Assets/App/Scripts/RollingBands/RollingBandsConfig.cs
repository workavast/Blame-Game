using UnityEngine;

namespace App.RollingBands
{
    [CreateAssetMenu(fileName = nameof(RollingBandsConfig), menuName = Consts.AppName + "/" + nameof(RollingBandsConfig))]
    public class RollingBandsConfig : ScriptableObject
    {
        [SerializeField] private Vector2 visibleValue = new(0.5f, 1);
        [SerializeField] private Vector2 invisibleValue = new(1, 1);
        [SerializeField] private float changeTime = 1;
        [SerializeField] private Material material;
        
        public Vector2 VisibleValue => visibleValue;
        public Vector2 InvisibleValue => invisibleValue;
        public float ChangeTime => changeTime;
        public Material Material => material;
    }
}