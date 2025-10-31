using System.Collections;
using App.MonoBehProviding;
using Avastrad.Libs.CheckOnNullLib;
using UnityEngine;

namespace App.RollingBands
{
    public class RollingBandsVisibilityChanger 
    {
        private static readonly int Remap = Shader.PropertyToID("_Remap");
        
        private readonly RollingBandsConfig _config;
        private readonly IMonoBehProvider _monoBehProvider;
        private float _timer;

        private Vector2 VisibleValue => _config.VisibleValue;
        private Vector2 InvisibleValue => _config.InvisibleValue;
        private float ChangeTime => _config.ChangeTime;
        private Material Material =>  _config.Material;

        private Coroutine _lastCoroutine;
        
        public RollingBandsVisibilityChanger(RollingBandsConfig config, IMonoBehProvider monoBehProvider)
        {
            _config = config;
            _monoBehProvider = monoBehProvider;
        }
        
        public void Toggle(bool isVisible)
        {
            if (_monoBehProvider.IsAnyNull())
            {
                Debug.LogWarning("monoBeh provider is null");
                return;
            }
            
            _monoBehProvider.StopCoroutine(_lastCoroutine);
            if (isVisible)
                _lastCoroutine = _monoBehProvider.StartCoroutine(Show());
            else
                _lastCoroutine = _monoBehProvider.StartCoroutine(Hide());
        }
        
        private IEnumerator Show()
        {
            do
            {
                _timer += Time.unscaledDeltaTime;
                var vector = Vector2.Lerp(InvisibleValue, VisibleValue, _timer / ChangeTime);
                Material.SetVector(Remap, vector);

                yield return new WaitForEndOfFrame();
            } while (_timer < ChangeTime);

            _timer = ChangeTime;
        }
        
        private IEnumerator Hide()
        {
            do
            {
                _timer -= Time.unscaledDeltaTime;
                var vector = Vector2.Lerp(InvisibleValue, VisibleValue, _timer / ChangeTime);
                Material.SetVector(Remap, vector);

                yield return new WaitForEndOfFrame();
            } while (_timer > 0);

            _timer = 0;
        }
    }
}