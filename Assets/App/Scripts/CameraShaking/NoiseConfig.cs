using Unity.Cinemachine;
using UnityEngine;

namespace App.CameraShaking
{
    [CreateAssetMenu(fileName = nameof(NoiseConfig), menuName = AppConsts.AppName + "/Configs/" + nameof(NoiseConfig))]
    public class NoiseConfig : ScriptableObject
    {
        [field: SerializeField] public NoiseSettings NoiseSettings { get; private set; }
        [field: SerializeField, Min(0)] public float TimeLenght { get; private set; } = 1;
    }
}