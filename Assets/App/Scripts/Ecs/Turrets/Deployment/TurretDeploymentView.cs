using System;
using App.Ecs.EntityViews;
using DG.Tweening;
using UnityEngine;

namespace App.Ecs.Turrets.Deployment
{
    public class TurretDeploymentView : MonoBehaviour, IEntityViewElement
    {
        [SerializeField] private TurretSphereView sphereView;
        [SerializeField] private Ease animationEase = Ease.Linear;
        
        public event Action<IEntityViewElement> OnCleanupCompleted;

        public bool OnDestroyCallback() 
            => true;
        
        public void SetDeployTime(float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            var easedValue = DOVirtual.EasedValue(0f, 1f, percentage, animationEase);
            
            sphereView.SetScale(easedValue);
        }
    }
}