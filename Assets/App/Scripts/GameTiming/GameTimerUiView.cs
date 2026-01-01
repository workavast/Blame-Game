using TMPro;
using UnityEngine;
using Zenject;

namespace App.GameTiming
{
    public class GameTimerUiView : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeViewTxt;

        [Inject] private readonly IGameTimerRO _gameTimer;
        
        private float _lastTimeValue;

        private void Update()
        {
            if (!Mathf.Approximately(_lastTimeValue, _gameTimer.RemainTime))
            {
                _lastTimeValue = _gameTimer.RemainTime;
                timeViewTxt.text = $"{Mathf.Floor(_gameTimer.RemainMinutes):00}:{Mathf.Floor(_gameTimer.RemainSeconds):00}";
            }
        }
    }
}