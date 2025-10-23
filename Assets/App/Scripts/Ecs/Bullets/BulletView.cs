using System.Collections;
using App.Ecs.Clenuping;
using Unity.Mathematics;
using UnityEngine;

namespace App.Ecs.Bullets
{
    public class BulletView : CleanupView
    {
        [SerializeField] private Light lighting;
        [SerializeField] private float fadeTime;
        
        public void SetPosition(float3 position)
            => transform.position = position;

        public void SetRotation(quaternion rotation)
            => transform.rotation = rotation;

        protected override void DestroyCallback()
        {
            StartCoroutine(Fade());
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
            
            base.DestroyCallback();
        }
    }
}