using TMPro;
using UnityEngine;
using Zenject;

namespace App.GameTiming
{
    public class GameTimerUiView : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeViewTxt;

        [Inject] private readonly GameTimer _gameTimer;
        
        private float _lastTimeValue;

        private void Update()
        {
            if (!Mathf.Approximately(_lastTimeValue, _gameTimer.Time))
            {
                _lastTimeValue = _gameTimer.Time;
                timeViewTxt.text = $"{Mathf.Floor(_gameTimer.Minutes):00}:{Mathf.Floor(_gameTimer.Seconds):00}";
            }
        }
    }
}