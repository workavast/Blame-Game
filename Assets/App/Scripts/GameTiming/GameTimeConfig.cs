using UnityEngine;

namespace App.GameTiming
{
    [CreateAssetMenu(fileName = nameof(GameTimeConfig), menuName = AppConsts.ConfigsPath + nameof(GameTimeConfig))]
    public class GameTimeConfig : ScriptableObject
    {
        [SerializeField] private float startTime;
        [SerializeField] private float winTime;

        public float StartTime => startTime;
        public float WinTime => winTime;
    }
}