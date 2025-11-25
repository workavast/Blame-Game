using System;
using App.Ecs.EntityViews;
using UnityEngine;

namespace App.Ecs.Death.Vfx
{
    public class DeathVfxView : MonoBehaviour, IEntityViewElement
    {
        [SerializeField] private GameObject model;
        [SerializeField] private ParticleProvider deathVfx;

        public event Action<IEntityViewElement> OnCleanupCompleted;

        public void Activate()
        {
            model.SetActive(false);
            deathVfx.Play();
        }

        public bool OnDestroyCallback()
        {
            deathVfx.OnStopped += () => OnCleanupCompleted?.Invoke(this);
            Activate();
            return false;
        }
    }
}