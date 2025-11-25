using System;
using System.Collections;
using App.Ecs.EntityViews;
using Unity.Mathematics;
using UnityEngine;

namespace App.Ecs.Bullets
{
    public class BulletView : MonoBehaviour, IEntityViewElement
    {
        [SerializeField] private Light lighting;
        [SerializeField] private float fadeTime;
        [SerializeField] private float showTime = 10;

        private Vector3 _initialScale;
        private float _initialLightIntensity;
        public event Action<IEntityViewElement> OnCleanupCompleted;

        private void Awake()
        {
            _initialScale = transform.localScale;
            _initialLightIntensity = lighting.intensity;
        }

        public void SetPosition(float3 position)
            => transform.position = position;

        public void SetRotation(quaternion rotation)
            => transform.rotation = rotation;

        private void OnEnable()
        {
            StopAllCoroutines();
            StartCoroutine(Show());
        }

        public bool OnDestroyCallback()
        {
            StopAllCoroutines();
            StartCoroutine(Fade());
            return false;
        }
        
        private IEnumerator Show()
        {
            var showTimer = showTime;
            var targetScale = _initialScale;
            var targetLightIntensity = _initialLightIntensity;
            
            while (showTimer > 0)
            {
                transform.localScale = targetScale * (1 - showTimer / showTime);
                lighting.intensity = targetLightIntensity * (1 - showTimer / showTime);
                
                showTimer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        
        private IEnumerator Fade()
        {
            var fadeTimer = fadeTime;
            var startScale = transform.localScale;
            var startLightIntensity = lighting.intensity;
            
            while (fadeTimer > 0)
            {
                transform.localScale = startScale * fadeTimer / fadeTime;
                lighting.intensity = startLightIntensity * fadeTimer / fadeTime;
                
                fadeTimer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            
            OnCleanupCompleted?.Invoke(this);
        }
    }
}